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

        [IsRegister("/FreeCoupon/CouponList")]
        public ActionResult CouponList()
        {
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());

            IEnumerable<MyFreeCoupon> mfcList = _myFreeCouponBLL.GetMyFreeCouponList(member.Id);
            if (mfcList != null && mfcList.Count() >0)
            {
                return RedirectToAction("Info", "UserInfo");
            }

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
                return RedirectToAction("Register", "Register");
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
                        string qrpath = CreateMyFreeCouponQR(mfc.Id.ToString());

                        mfc.FreeCouponQRs = qrpath;

                        db.Set<MyFreeCoupon>().Attach(mfc);
                        db.Entry(mfc).State = System.Data.Entity.EntityState.Modified;

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
            MyFreeCoupon mfc = _myFreeCouponBLL.GetMyFreeCouponById(myFreeCouponId);
            MyFreeCouponModel mfcm = new MyFreeCouponModel();
            FreeCoupon fc = _freeCouponBLL.GetFreeCouponById(mfc.FreeCouponId);

            mfcm.myFreeCouponId = mfc.Id;
            mfcm.FreeCouponName = fc.CouponName;
            mfcm.CreatedTime = mfc.CreatedTime;

            ViewBag.MyFreeCouponPath = mfc.FreeCouponQRs;

            ViewBag.MyFreeCouponModel = mfcm;

            return View();
        }
        private string CreateMyFreeCouponQR(string freeCouponId)
        {
            try
            {
                string dir = Server.MapPath("~/MyFreeCouponQRs/");
                string sourceString = "http://jcb.ybtx88.com/Pay/PayForFreeCoupon?myFreeCouponId=" + freeCouponId;
                string qrPath = QRCodeCreator.Create(sourceString, dir);

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