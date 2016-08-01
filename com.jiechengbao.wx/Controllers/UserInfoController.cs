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
        private ITransactionBLL _transactionBLL;
        private IOrderBLL _orderBLL;
        private ICreditRecordBLL _creditRecordBLL;
        private IAppointmentServiceBLL _appointmentServiceBLL;
        private IAppointmentTimeBLL _appointmentTimeBLL;
        public UserInfoController(IMemberBLL memberBLL, IGoodsBLL goodsBLL,
            IGoodsImagesBLL goodsImagesBLL, IReCommendBLL recommendBLL, 
            IRechargeBLL rechargeBLL,ICarBLL carBLL, IServiceBLL serviceBLL,
            IExchangeServiceRecordBLL exchangeServiceRecordBLL, 
            IExchangeServiceBLL exchangeServiceBLL, 
            ITransactionBLL transactionBLL, IOrderBLL orderBLL,
            ICreditRecordBLL creditRecordBLL,IAppointmentServiceBLL appointmentServiceBLL,
            IAppointmentTimeBLL appointmentTimeBLL)
        {
            _memberBLL = memberBLL;
            _goodsBLL = goodsBLL;
            _goodsImageBLL = goodsImagesBLL;
            _recommendBLL = recommendBLL;
            _rechargeBLL = rechargeBLL;
            _carBLL = carBLL;
            _serviceBLL = serviceBLL;
            _exchangeServiceBLL = exchangeServiceBLL;
            _exchangeServiceRecordBLL = exchangeServiceRecordBLL;
            _transactionBLL = transactionBLL;
            _orderBLL = orderBLL;
            _creditRecordBLL = creditRecordBLL;
            _appointmentServiceBLL = appointmentServiceBLL;
            _appointmentTimeBLL = appointmentTimeBLL;
        }

        /// <summary>
        ///  会员首页
        /// </summary>
        /// <returns></returns>
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
        
        /// <summary>
        /// 紧急救援
        /// </summary>
        /// <returns></returns>
        public ActionResult Help()
        {
            return View();
        }
        
        /// <summary>
        /// 违章查询   不用的
        /// </summary>
        /// <returns></returns>
        public ActionResult Check()
        {
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());
            List<Car> carList = _carBLL.GetCarListByMemberId(member.Id).ToList();

            ViewData["carList"] = carList;

            return View();
        }

        /// <summary>
        /// 个人信息
        /// </summary>
        /// <returns></returns>
        [IsLogin]
        [IsRegister]
        public ActionResult Info()
        {
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());
            return View(member);
        }
        
        /// <summary>
        /// 我的二维码  其实是公共账号的二维码  并没什么作用
        /// </summary>
        /// <returns></returns>
        public ActionResult MyQRCode()
        {
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());
            return View(member);
        }

        public ActionResult MyLocation()
        {
            //string appid = "wxf57a5eab31b46755";
            //string timestamp = Convert.ToInt64((DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds).ToString();
            //string nonceStr = "ikrmvjcuelopsxjf";
            //string signature = string.Empty;
            //string url = HttpContext.Request.Url.AbsoluteUri;


            //if (CacheManager.GetCache("JSAPI_Ticket") == null || (CacheManager.GetCache("JSAPI_Ticket") as com.ybtx.mobile.Global.JsAPI_Ticket).time.AddSeconds(7200) <= DateTime.Now)
            //{
            //    if (CacheManager.GetCache("Token") == null || (CacheManager.GetCache("Token") as Global_Token).time.AddSeconds(7200) <= DateTime.Now)
            //    {
            //        BaseToken bt = new WxHelper.WxHelper().GetBaseToken();
            //        Global_Token gt = new Global_Token();
            //        gt.token = bt;
            //        gt.time = DateTime.Now;

            //        CacheManager.SetCache("Token", gt);

            //        WxHelper.WxHelper wx = new WxHelper.WxHelper();
            //        Global.JsAPI_Ticket ticket = new Global.JsAPI_Ticket();

            //        WxHelper.JsAPI_Ticket tk = wx.GetJsAPITicket(bt.access_token);

            //        ticket.ticket = tk;
            //        ticket.time = DateTime.Now;

            //        CacheManager.SetCache("JSAPI_Ticket", ticket);
            //        signature = CalSignature.Cal(timestamp, nonceStr, ticket.ticket.ticket, url);
            //    }
            //    else
            //    {
            //        Global.JsAPI_Ticket ticket = new Global.JsAPI_Ticket();
            //        WxHelper.JsAPI_Ticket tk = new WxHelper.WxHelper().GetJsAPITicket((CacheManager.GetCache("Token") as Global_Token).token.access_token);

            //        ticket.ticket = tk;
            //        ticket.time = DateTime.Now;

            //        CacheManager.SetCache("JSAPI_Ticket", ticket);
            //        signature = CalSignature.Cal(timestamp, nonceStr, ticket.ticket.ticket, url);
            //    }
            //}
            //else
            //{
            //    signature = CalSignature.Cal(timestamp, nonceStr, (CacheManager.GetCache("JSAPI_Ticket") as Global.JsAPI_Ticket).ticket.ticket, url);
            //}

            //ShareInfo info = new ShareInfo();
            //info.appid = appid;
            //info.nonceStr = nonceStr;
            //info.timestamp = timestamp;
            //info.signature = signature;

            //System.Web.HttpContext.Current.Session["ShareInfo"] = info;
        
            return View();
        }
        
        /// <summary>
        /// 电话绑定
        /// </summary>
        /// <returns></returns>
        public ActionResult Phone()
        {
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());
            ViewBag.Phone = member.Phone;
            return View();
        }

        /// <summary>
        /// 我的服务
        /// </summary>
        /// <returns></returns>
        [IsRegister]
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

        #region 积分商城里面的东西已经不用了

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

            ViewData["MyCreditServices"] = eslmList.OrderByDescending(n=>n.CreatedTime).ToList();

            return View();
        }

        #endregion

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

        /// <summary>
        /// 会员充值
        /// </summary>
        /// <returns></returns>
        [HttpPost]
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
        
        /// <summary>
        /// 访问前 先判断是否已注册
        /// </summary>
        /// <returns></returns>
        [IsRegister]
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

        public ActionResult TransactionRecordList()
        {
            // 先找到当前用户
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());

            // 再找到当前用户下的最近30条余额交易记录
            List<Transaction> transList = _transactionBLL.GetTransactionByMemberIdwithCount(member.Id, 30).ToList();
            //
            //  构造出TransactionModelList  并返回
            List<TransactionModel> modelList = new List<TransactionModel>();

            foreach (var item in transList)
            {
                TransactionModel tm = new TransactionModel();
                tm.Amount = item.Amount;
                tm.CreateTime = item.CreatedTime;
                Order order = _orderBLL.GetOrderByOrderId(item.OrderId);
                tm.OrderNo = order.OrderNo;

                modelList.Add(tm);
            }
            ViewData["TransactionModelList"] = modelList;
            
            return View();
        }

        /// <summary>
        /// 我的积分  积分的来源和去向记录  只显示最近一个月的记录
        /// </summary>
        /// <returns></returns>
        [IsRegister]
        public ActionResult MyCreditRecord(double currentCredit)
        {
            if (currentCredit <= 0)
            {
                ViewBag.CurrentCredit = 0;
            }
            ViewBag.CurrentCredit = currentCredit;

            // 上来先获取当前用户对象
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());

            List<CreditRecord> crList = _creditRecordBLL.GetCreditRecordByMemberId(member.Id).ToList();

            ViewData["creditRecordList"] = crList;
            return View();
        }
        
        #region 我的预约

        /// <summary>
        /// 预约页面
        /// </summary>
        /// <returns></returns>
        public ActionResult MyAppointment()
        {
            // 上来先获取当前用户信息
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());

            // 获取车辆信息
            List<Car> carList = _carBLL.GetCarListByMemberId(member.Id).ToList();

            // 获取预约服务列表
            List<AppointmentService> appointmentList = _appointmentServiceBLL.GetAllAppointmentServiceButNotDeleted().ToList();

            List<AppointmentTime> appointmentTimeList = _appointmentTimeBLL.GetLastAppointmentTimeList().ToList();

            ViewData["carList"] = carList;
            ViewData["appointmentList"] = appointmentList;
            ViewData["appointmentTimeList"] = appointmentTimeList;

            return View();
        }

        #endregion
    }
}