using ch.lib.common.QR;
using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using com.jiechengbao.wx.Global;
using com.jiechengbao.wx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.wx.Controllers
{
    public class FreeCouponController:Controller
    {
        private IFreeCouponBLL _freeCouponBLL;
        private IMyFreeCouponBLL _myFreeCouponBLL;
        private IMemberBLL _memberBLL;
        public FreeCouponController(IFreeCouponBLL freeCouponBLL, IMemberBLL memberBLL, IMyFreeCouponBLL myFreeCouponBLL)
        {
            _freeCouponBLL = freeCouponBLL;
            _memberBLL = memberBLL;
            _myFreeCouponBLL = myFreeCouponBLL;
        }

        public ActionResult CouponList()
        {
            //Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());

            //IEnumerable<MyFreeCoupon> mfcList = _myFreeCouponBLL.GetMyFreeCouponList(member.Id);
            //if (mfcList != null && mfcList.Count() >0)
            //{
            //    return RedirectToAction("Info", "UserInfo");
            //}

            ViewData["CouponList"] = _freeCouponBLL.GetAllNotDeletedCoupon();
            return View();
        }

        /// <summary>
        /// 领取优惠券
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PickUp(string Code)
        {
            bool res = false;

            if (string.IsNullOrEmpty(Code))
            {
                var obj = new
                {
                    code = res,
                    msg = "非法的参数, 请提交正确的参数"
                };

                return Json(obj, JsonRequestBehavior.AllowGet);
            }

            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"] as string);

            if (member == null)
            {
                var obj = new {
                    code = res,
                    msg = "无法确认用户信息，请重新访问"
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(member.Phone))
            {
                var obj = new
                {
                    code = "Reg"
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }

            try
            {
                // 验证重复领取
                if (_myFreeCouponBLL.IsAlreadyPicked(member.Id))
                {
                    var obj = new
                    {
                        code = res,
                        msg = "您已经领取过优惠券了，现在不能领取了"
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);

                var obj = new {
                    code = false,
                    msg = "系统错误，领取失败"
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            

            FreeCoupon fc = _freeCouponBLL.GetFreeCouponByCode(Code);

            if (fc == null)
            {
                var obj = new
                {
                    code = res,
                    msg = "无法查询到相应的活动优惠券"
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }

            MyFreeCoupon mfc = new MyFreeCoupon();
            mfc.Id = Guid.NewGuid();
            mfc.CreatedTime = DateTime.Now;
            mfc.DeletedTime = DateTime.MinValue.AddHours(8);
            mfc.FreeCouponId = fc.Id;
            mfc.IsDeleted = false;
            mfc.MemberId = member.Id;
            mfc.FreeCouponQRs = string.Empty;

            bool code = false;
            string msg = string.Empty;
            
            // 启用事务 添加新的我的优惠券 及其 消费二维码的生成

            using (JCB_DBContext db = new JCB_DBContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Set<MyFreeCoupon>().Add(mfc);
                        db.SaveChanges();

                        trans.Commit();

                        code = true;
                        msg = "领取成功";
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        code = false;
                        msg = "领取失败";

                        LogHelper.Log.Write(ex.Message);
                        LogHelper.Log.Write(ex.StackTrace);
                    }
                }
            }

            var resobj = new { code = code, msg = msg };
            return Json(resobj, JsonRequestBehavior.AllowGet);   
        }

        public ActionResult MyFreeCouponList()
        {
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());
            IEnumerable<MyFreeCoupon> myFreeCouponList = _myFreeCouponBLL.GetMyFreeCouponList(member.Id);

            List<MyFreeCouponModel> myFreeCouponModelList = new List<Models.MyFreeCouponModel>();

            foreach (var item in myFreeCouponList)
            {
                MyFreeCouponModel mfcm = new Models.MyFreeCouponModel();
                FreeCoupon fc = _freeCouponBLL.GetFreeCouponById(item.FreeCouponId);

                if (fc == null)
                {
                    continue;
                }
                mfcm.myFreeCouponId = item.Id;
                mfcm.FreeCouponName = fc.CouponName;
                mfcm.Price = fc.Price;
                mfcm.Descritption = fc.Description;
                mfcm.CreatedTime = item.CreatedTime;

                myFreeCouponModelList.Add(mfcm);
            }

            ViewData["MyFreeCouponModelList"] = myFreeCouponModelList;

            return View();
        }

        public ActionResult MyFreeCouponQR(Guid myFreeCouponId)
        {
            try
            {
                MyFreeCoupon mfc = _myFreeCouponBLL.GetMyFreeCouponById(myFreeCouponId);
                MyFreeCouponModel mfcm = new MyFreeCouponModel();
                FreeCoupon fc = _freeCouponBLL.GetFreeCouponById(mfc.FreeCouponId);

                mfcm.myFreeCouponId = mfc.Id;
                mfcm.FreeCouponName = fc.CouponName;
                mfcm.CreatedTime = mfc.CreatedTime;

                // 动态二维码的地址
                // 先判断 当前 会话 中 优惠券二维码的缓存 是否为空
                if (System.Web.HttpContext.Current.Session["FreeCouponQrPath"] == null)
                {
                    string sessionId = System.Web.HttpContext.Current.Session["SessionID"].ToString();
                    // 如果为空 则生成新的二维码
                    ViewBag.QrPath = CreateMyFreeCouponQR(myFreeCouponId.ToString(), sessionId);
                }
                else
                {
                    // 不为空时 直接用缓存二维码
                    ViewBag.QrPath = System.Web.HttpContext.Current.Session["FreeCouponQrPath"].ToString();
                }

               // ViewBag.MyFreeCouponPath = mfc.FreeCouponQRs;

                ViewBag.MyFreeCouponModel = mfcm;

                return View();
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);

                return RedirectToAction("ServerError", "Error");
            }
            
        }
        private string CreateMyFreeCouponQR(string freeCouponId,string sessionId)
        {
            try
            {
                string dir = Server.MapPath("~/MyFreeCouponQRs/");
                string sourceString = "http://"+wx.Models.WxConfig.WxDomain+"/Pay/PayForFreeCoupon?myFreeCouponId=" + freeCouponId + "&sessionId=" + sessionId; ;
                string qrPath = QRCodeCreator.Create(sourceString, dir);

                System.Web.HttpContext.Current.Session["FreeCouponQrPath"] = qrPath;
                return qrPath;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }
    }
}