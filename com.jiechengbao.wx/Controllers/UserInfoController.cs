using com.jiechengbao.common;
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
    
    public class UserInfoController : Controller
    {
        private IMemberBLL _memberBLL;
        private IGoodsBLL _goodsBLL;
        private IGoodsImagesBLL _goodsImageBLL;
        private IReCommendBLL _recommendBLL;
        private IRechargeBLL _rechargeBLL;
        private ICarBLL _carBLL;
        private IServiceBLL _serviceBLL;
        private IExchangeServiceRecordBLL _exchangeServiceRecordBLL;
        private IExchangeServiceBLL _exchangeServiceBLL;
        public UserInfoController(IMemberBLL memberBLL, IGoodsBLL goodsBLL,
            IGoodsImagesBLL goodsImagesBLL, IReCommendBLL recommendBLL, 
            IRechargeBLL rechargeBLL,ICarBLL carBLL, IServiceBLL serviceBLL,
            IExchangeServiceRecordBLL exchangeServiceRecordBLL, IExchangeServiceBLL exchangeServiceBLL)
        {
            _memberBLL = memberBLL;
            _goodsBLL = goodsBLL;
            _goodsImageBLL = goodsImagesBLL;
            _recommendBLL = recommendBLL;
            _rechargeBLL = rechargeBLL;
            _carBLL = carBLL;
            _serviceBLL = serviceBLL;
            _exchangeServiceBLL = exchangeServiceBLL;
        }
        [IsLogin]
        public ActionResult Index()
        {
            // 先获得推荐商品的索引 即商品的guid list
            IEnumerable<ReCommend> recommendList = _recommendBLL.GetAllReCommendListwithSortByTime();

            List<GoodsModel> goodsModelList = new List<GoodsModel>();
            foreach (var item in recommendList)
            {
                Goods goods = _goodsBLL.GetGoodsById(item.GoodsId);
                if (goods == null)
                {
                    LogHelper.Log.Write("goods is null");
                }

                GoodsImage gi = _goodsImageBLL.GetPictureByGoodsId(goods.Id);

                if (gi == null)
                {
                    LogHelper.Log.Write("gi is null");
                }

                GoodsModel gm = new GoodsModel(goods);
                gm.PicturePath = gi.ImagePath;

                goodsModelList.Add(gm);
            }

            // 要显示的推荐商品
            ViewData["GoodsModelList"] = goodsModelList;
            return View();
        }
        
        public ActionResult Help()
        {
            return View();
        }
        
        public ActionResult Check()
        {
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());
            List<Car> carList = _carBLL.GetCarListByMemberId(member.Id).ToList();

            ViewData["carList"] = carList;

            return View();
        }

        [IsLogin]
        public ActionResult Info()
        {
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());
            return View(member);
        }
        public ActionResult Phone()
        {
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());
            ViewBag.Phone = member.Phone;
            return View();
        }
        
        public ActionResult MyServices()
        {
            List<MyService> msList = new List<MyService>();
            List<ServiceDetailModel> sdmList = new List<ServiceDetailModel>();

          
            string openId = System.Web.HttpContext.Current.Session["member"].ToString();

            Member member = _memberBLL.GetMemberByOpenId(openId);

            try
            {
                IEnumerable<MyService> mstmpList = _serviceBLL.GetMyServiceByMemberId(member.Id);

                if (mstmpList != null)
                {
                    LogHelper.Log.Write("mstmpList 不为空");
                    msList = mstmpList.ToList();
                }
                else
                {
                    LogHelper.Log.Write("mstmpList == null");
                }

            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }

            foreach (var item in msList)
            {
                
                ServiceDetailModel sdm = new ServiceDetailModel();
                sdm.CurrentCount = item.CurrentCount;
                sdm.ServcieId = item.Id;
                sdm.ServiceImagePath = _goodsImageBLL.GetPictureByGoodsId(item.GoodsId).ImagePath;
                sdm.ServiceName = item.GoodsName;
                sdm.TotalCount = item.TotalCount;

                sdmList.Add(sdm);
            }
            ViewData["SDMList"] = sdmList;

            return View();
        }

        /// <summary>
        ///  我的积分 已购买的兑换商品列表
        /// </summary>
        /// <returns></returns>
        public ActionResult MyCreditServices()
        {
            // 同样先 获得当前用户对象
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());

            // 根据用户id 获取已兑换的商品列表
            IEnumerable<ExchangeServiceRecord> esrList = _exchangeServiceRecordBLL.GetMyESR(member.Id);

            List<ExchangeServiceListModel> eslmList = new List<ExchangeServiceListModel>();

            // 构造 ExchangeServiceListModel 列表
            foreach (var item in esrList)
            {
                ExchangeServiceListModel eslm = new ExchangeServiceListModel();

                eslm.CreatedTime = item.CreatedTime;
                eslm.ExchangeServiceRecordId = item.Id;
                ExchangeService es = _exchangeServiceBLL.GetNoDeletedExchangeServiceById(item.ExchangeSerivceId);

                eslm.ExchangeServiceName = es.Name;
                eslm.ImagePath = es.ImagePath;

                eslmList.Add(eslm);
            }

            ViewData["MyCreditServices"] = eslmList;

            return View();
        }

        [HttpPost]
        public JsonResult Phone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());
            member.Phone = phone;

            if (_memberBLL.Update(member))
            {
                return Json("True", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
        }
        
        public ActionResult Recharge()
        {
            return View();
        }

        // 业务修改  不需要主动搜索充值记录
        // 这个方法就不用了
        [HttpPost]
        public ActionResult CheckRecharge(DateTime startTime, DateTime endTime)
        {
            // 上来先获取 当前用户 的对象
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());
            if (startTime == null || endTime == null)
            {
                var res = new
                {
                    startTime = startTime,
                    endTime = endTime
                };
                return Json(res, JsonRequestBehavior.AllowGet);
            }

            List<Recharge> rechargeList = _rechargeBLL.GetRechargeListByMemberId(startTime,endTime,member.Id).ToList();
            System.Web.HttpContext.Current.Session["RechargeList"] = rechargeList;

            return Json("True", JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult RechargeList()
        {
            // 业务修改 不用用户自己搜索充值记录
            // 在客户端只显示充值记录的前10条

            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());

            if (member == null)
            {
                ViewData["RechargeList"] = null;
                return View();
            }

            List<Recharge> rechargeList = _rechargeBLL.GetRechargeListTop10ByMemberId(member.Id).ToList();

            ViewData["RechargeList"] = rechargeList;

            return View();

            //if (null == (System.Web.HttpContext.Current.Session["RechargeList"] as List<Recharge>))
            //{
            //    return View();
            //}
            //else
            //{
            //    ViewData["RechargeList"] = System.Web.HttpContext.Current.Session["RechargeList"] as List<Recharge>;
            //    return View();
            //}
        }
        
        public ActionResult RechargeOptionsList()
        {
            return View();
        }

        public ActionResult InfoDetail(string openId)
        {
            if (string.IsNullOrEmpty(openId))
            {
                return View();
            }

            Member member = _memberBLL.GetMemberByOpenId(openId);

            if (member == null)
            {
                return View();
            }
            return View(member);
        }
    }
}