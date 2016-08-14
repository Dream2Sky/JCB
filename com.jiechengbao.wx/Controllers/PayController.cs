
// 写在前面

// 支付类型  目前交易类型 分为 两个 

// 微信支付 和 余额支付

// 0 表示 微信支付 ，  1 表示 余额支付

using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using com.jiechengbao.wx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using com.jiechengbao.common;
using System.Text;
using WxPayAPI;
using System.Configuration;
using ch.lib.common.QR;
using com.jiechengbao.wx.Global;

namespace com.jiechengbao.wx.Controllers
{

    public class PayController : Controller
    {
        /// <summary>
        /// 升级VIP委托
        /// </summary>
        /// <param name="memberId"></param>
        private delegate void UpGradeDel(Guid memberId);

        /// <summary>
        /// 添加我的服务委托
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="orderNO"></param>
        private delegate void AddMyServiceDel(Guid memberId, string orderNO);

        private IMemberBLL _memberBLL;
        private IOrderBLL _orderBLL;
        private ITransactionBLL _transactionBLL;
        private IRechargeBLL _recharegeBLL;
        private ICreditRecordBLL _creditRecordBLL;
        private IRulesBLL _rulesBLL;
        private IOrderDetailBLL _orderDetailBLL;
        private IGoodsBLL _goodsBLL;
        private IServiceBLL _serviceBLL;
        private IServiceQRBLL _serviceQRBLL;
        private IServiceConsumeRecordBLL _serviceConsumeRecoredBLL;
        private IServiceConsumePasswordBLL _serviceConsumePasswordBLL;
        private IExchangeServiceRecordBLL _exchangeServiceRecordBLL;
        private IExchangeServiceBLL _exchangeServiceBLL;
        private IExchangeServiceQRBLL _exchangeServiceQRBLL;
        public PayController(IMemberBLL memberBLL, IOrderBLL orderBLL,
            ITransactionBLL transactionBLL, IRechargeBLL rechargeBLL,
            ICreditRecordBLL creditRecordBLL, IRulesBLL rulesBLL,
            IOrderDetailBLL orderDetailBLL, IGoodsBLL goodsBLL,
            IServiceBLL serviceBLL, IServiceQRBLL serviceQRBLL,
            IServiceConsumeRecordBLL serviceConsumeRecordBLL,
            IServiceConsumePasswordBLL serviceConsumePasswordBLL,
            IExchangeServiceRecordBLL exchangeServiceRecordBLL,
            IExchangeServiceBLL exchangeServiceBLL,
            IExchangeServiceQRBLL exchangeServiceQRBLL)
        {
            _memberBLL = memberBLL;
            _orderBLL = orderBLL;
            _transactionBLL = transactionBLL;
            _recharegeBLL = rechargeBLL;
            _creditRecordBLL = creditRecordBLL;
            _orderDetailBLL = orderDetailBLL;
            _goodsBLL = goodsBLL;
            _serviceBLL = serviceBLL;
            _rulesBLL = rulesBLL;
            _serviceQRBLL = serviceQRBLL;
            _serviceConsumeRecoredBLL = serviceConsumeRecordBLL;
            _serviceConsumePasswordBLL = serviceConsumePasswordBLL;
            _exchangeServiceRecordBLL = exchangeServiceRecordBLL;
            _exchangeServiceBLL = exchangeServiceBLL;
            _exchangeServiceQRBLL = exchangeServiceQRBLL;
        }

        /// <summary>
        ///  微信支付专用逻辑
        /// </summary>
        /// <returns></returns>
        public ActionResult WxPay()
        {
            string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";

            ViewBag.OrderNo = Request.QueryString["orderNo"].ToString();
            ViewBag.TotalPrice = double.Parse(Request.QueryString["totalprice"].ToString());

            OrderResultModel orm = new OrderResultModel();
            orm.openid = System.Web.HttpContext.Current.Session["member"].ToString();
            orm.total_fee = double.Parse(Request.QueryString["totalprice"].ToString()) * 100;
            orm.trade_type = "JSAPI";
            orm.spbill_create_ip = Request.QueryString["ip"].ToString();
            orm.out_trade_no = Request.QueryString["orderNo"].ToString();
            orm.appid = WxPayAPI.WxPayConfig.APPID;
            orm.body = "捷诚宝商城";
            orm.mch_id = WxPayAPI.WxPayConfig.MCHID;
            orm.nonce_str = WxPayAPI.WxPayApi.GenerateNonceStr();
            orm.notify_url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Host + ":" + HttpContext.Request.Url.Port + "/Pay/WxPayResult";

            WxPayAPI.WxPayData data = new WxPayAPI.WxPayData();
            data.SetValue("openid", orm.openid);
            data.SetValue("total_fee", orm.total_fee);
            data.SetValue("trade_type", orm.trade_type);
            data.SetValue("spbill_create_ip", orm.spbill_create_ip);
            data.SetValue("out_trade_no", orm.out_trade_no);
            data.SetValue("appid", orm.appid);
            data.SetValue("body", orm.body);
            data.SetValue("mch_id", orm.mch_id);
            data.SetValue("nonce_str", orm.nonce_str);
            data.SetValue("notify_url", orm.notify_url);



            orm.sign = data.MakeSign();


            data.SetValue("sign", orm.sign);

            foreach (var item in data.GetValues())
            {
                LogHelper.Log.Write(item.Key + ":" + item.Value);
            }

            string xml = data.ToXml();
            string response = WxPayAPI.HttpService.Post(xml, url, false, 5);

            WxPayAPI.WxPayData result = new WxPayAPI.WxPayData();
            result.FromXml(response);

            WxPayAPI.WxPayData jsApiParam = new WxPayAPI.WxPayData();
            jsApiParam.SetValue("appId", result.GetValue("appid"));
            jsApiParam.SetValue("timeStamp", WxPayAPI.WxPayApi.GenerateTimeStamp());
            jsApiParam.SetValue("nonceStr", WxPayAPI.WxPayApi.GenerateNonceStr());
            jsApiParam.SetValue("package", "prepay_id=" + result.GetValue("prepay_id"));
            jsApiParam.SetValue("signType", "MD5");
            jsApiParam.SetValue("paySign", jsApiParam.MakeSign());

            string jsonParam = jsApiParam.ToJson();
            ViewData["Result"] = result;
            ViewData["JsonResult"] = jsonParam;

            return View();
        }

        [HttpPost]
        public void WxPayResult()
        {
            //接收从微信后台POST过来的数据
            System.IO.Stream s = Request.InputStream;
            int count = 0;
            byte[] buffer = new byte[1024];
            StringBuilder builder = new StringBuilder();
            while ((count = s.Read(buffer, 0, 1024)) > 0)
            {
                builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
            }
            s.Flush();
            s.Close();
            s.Dispose();


            //转换数据格式并验证签名
            WxPayData data = new WxPayData();
            try
            {
                data.FromXml(builder.ToString());
                Order order = _orderBLL.GetOrderByOrderNo(data.GetValue("out_trade_no").ToString());
                order.Status = 1;
                order.PayTime = DateTime.Now;

                Member member = _memberBLL.GetMemberByOpenId(data.GetValue("openid").ToString());

                if (_orderBLL.Update(order))
                {
                    // 添加交易记录
                    Transaction trans = new Transaction();
                    trans.Amount = order.TotalPrice;
                    trans.CreatedTime = DateTime.Now;
                    trans.DeletedTime = DateTime.MinValue.AddHours(8);
                    trans.Id = Guid.NewGuid();
                    trans.IsDeleted = false;
                    trans.MemberId = member.Id;
                    trans.OrderId = order.Id;
                    trans.PayWay = 0;

                    _transactionBLL.Add(trans);


                    if (AddConsumeCredit(member, order.TotalPrice,true))
                    {
                        // 异步判断是否有足够的积分进行升级
                        UpGradeDel del = new UpGradeDel(UpGradeVIP);
                        IAsyncResult result = del.BeginInvoke(member.Id, CallBackMethod, null);

                        // 异步找到此订单所有的服务  并添加
                        AddMyServiceDel msDel = new AddMyServiceDel(AddMyService);
                        IAsyncResult ra = msDel.BeginInvoke(member.Id, order.OrderNo, MyServiceCallBackMethod, null);
                    }
                }
                else
                {
                    LogHelper.Log.Write("支付失败");
                }

            }
            catch (WxPayException ex)
            {
                //若签名错误，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", ex.Message);
                Log.Error(this.GetType().ToString(), "Sign check error : " + res.ToXml());

                Response.Write(res.ToXml());
                Response.End();
            }

            Log.Info(this.GetType().ToString(), "Check sign success");
            WxPayData successData = new WxPayData();
            successData.SetValue("return_code", "SUCCESS");
            successData.SetValue("return_msg", "OK");

            Response.Write(successData.ToXml());
            Response.End();
            //return Content(successData.ToXml());
        }

        /// <summary>
        /// 余额支付专用逻辑
        /// </summary>
        /// <returns></returns>
        public ActionResult BalancePay()
        {
            if (Request.QueryString["orderNo"] == null)
            {
                return Content("<span style='color:#FF0000;font-size:20px'>" + "页面传参出错,请返回重试" + "</span>");
            }

            string orderNo = Request.QueryString["orderNo"].ToString();

            Order order = _orderBLL.GetOrderByOrderNo(orderNo);

            return View(order);
        }

        /// <summary>
        /// 真正操作余额的方法
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Pay(string orderNo)
        {
            // 上来先判断 orderNo 是否为空 
            // 如果为空  则直接返回 false
            if (string.IsNullOrEmpty(orderNo))
            {
                var res = new
                {
                    isSuccess = false,
                    msg = "提交的参数有误"
                };
                return Json(res, JsonRequestBehavior.AllowGet);
            }

            #region 更新订单状态

            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());

            // 取得 order对象 
            Order order = _orderBLL.GetOrderByOrderNo(orderNo);

            // 判断当前用户的当前积分 是否够 支付 

            // 余额取消了 改为用积分购买

            #region 判断余额是否充足
            // 判断余额 是否 充值
            if (member.Credit < order.TotalPrice)
            {
                var res = new
                {
                    isSuccess = false,
                    msg = "当前积分不足支付该商品,请及时充值,或使用其他支付方式"
                };
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            #endregion

            #region 判断订单状态
            if (order.Status == 1)
            {
                var res = new
                {
                    isSuccess = false,
                    msg = "此订单已支付，请不要重复提交，如有需要，请重新下单购买"
                };
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            #endregion

            // 修改order对象 
            order.PayWay = 1;
            order.PayTime = DateTime.Now;
            order.Status = 1;
            #endregion

            #region 添加交易记录

            //更新订单状态
            if (_orderBLL.Update(order))
            {
                // 添加交易记录
                Transaction trans = new Transaction();
                trans.Amount = order.TotalPrice;
                trans.CreatedTime = DateTime.Now;
                trans.DeletedTime = DateTime.MinValue.AddHours(8);
                trans.Id = Guid.NewGuid();
                trans.IsDeleted = false;
                trans.MemberId = member.Id;
                trans.OrderId = order.Id;
                trans.PayWay = 1;

                // 如果添加交易记录失败

                // 则回滚订单状态 并返回 false
                if (!_transactionBLL.Add(trans))
                {
                    order.Status = 0;
                    _orderBLL.Update(order);

                    var res = new
                    {
                        isSuccess = false,
                        msg = "更新数据失败"
                    };
                    return Json(res, JsonRequestBehavior.AllowGet);
                }

                try
                {
                    if (AddConsumeCredit(member, order.TotalPrice, false))
                    {
                        // 当修改消费积分成功时 异步判断是否够积分升级vip
                        //UpGradeDel del = new UpGradeDel(UpGradeVIP);
                        //IAsyncResult ra = del.BeginInvoke(member.Id, CallBackMethod, null);

                        //UpGradeVIP(member.Id);

                        // 异步找到服务商品 并添加
                        //AddMyServiceDel msDel = new AddMyServiceDel(AddMyService);
                        //IAsyncResult msResult = msDel.BeginInvoke(member.Id, order.OrderNo, MyServiceCallBackMethod, null);

                        AddMyService(member.Id, order.OrderNo);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Log.Write(ex.Message);
                    LogHelper.Log.Write(ex.StackTrace);
                    throw;
                }
                //添加消费积分记录 并修改会员积分
                


                //// 修改账户余额  修改账户当前余额
                //member.Credit = member.Credit - order.TotalPrice;


                if (!_memberBLL.Update(member))
                {
                    // 修改余额失败
                    // 则回滚 订单状态 和 交易记录
                    order.Status = 0;
                    _orderBLL.Update(order);
                    _transactionBLL.Remove(trans);
                    var res = new
                    {
                        isSuccess = false,
                        msg = "数据库扣款失败"
                    };
                    return Json(res, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                // 更新订单状态失败
                var res = new
                {
                    isSuccess = false,
                    msg = "更新数据失败"
                };
                return Json(res, JsonRequestBehavior.AllowGet);
            }

            #endregion
            var json = new
            {
                isSuccess = true,
                msg = "支付成功"
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RechargePay(double money, string ip)
        {
            string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";

            string orderNo = Guid.NewGuid().ToString().Replace("-", "").ToUpper().Substring(0, 6) + TimeManager.GetCurrentTimestamp();
            ViewBag.OrderNo = orderNo;
            ViewBag.TotalPrice = money;

            OrderResultModel orm = new OrderResultModel();
            orm.openid = System.Web.HttpContext.Current.Session["member"].ToString();
            orm.total_fee = money * 100;
            orm.trade_type = "JSAPI";
            orm.spbill_create_ip = ip;
            orm.out_trade_no = orderNo;
            orm.appid = WxPayAPI.WxPayConfig.APPID;
            orm.body = "捷诚宝个人中心充值";
            orm.mch_id = WxPayAPI.WxPayConfig.MCHID;
            orm.nonce_str = WxPayAPI.WxPayApi.GenerateNonceStr();

            orm.notify_url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Host + ":" + HttpContext.Request.Url.Port + "/Pay/RechargePayResult";

            LogHelper.Log.Write(orm.notify_url);

            WxPayAPI.WxPayData data = new WxPayAPI.WxPayData();
            data.SetValue("openid", orm.openid);
            data.SetValue("total_fee", orm.total_fee);
            data.SetValue("trade_type", orm.trade_type);
            data.SetValue("spbill_create_ip", orm.spbill_create_ip);
            data.SetValue("out_trade_no", orm.out_trade_no);
            data.SetValue("appid", orm.appid);
            data.SetValue("body", orm.body);
            data.SetValue("mch_id", orm.mch_id);
            data.SetValue("nonce_str", orm.nonce_str);
            data.SetValue("notify_url", orm.notify_url);



            orm.sign = data.MakeSign();


            data.SetValue("sign", orm.sign);

            //LogHelper.Log.Write("openid:" + data.GetValue("openid"));
            //LogHelper.Log.Write("total_fee:" + data.GetValue("total_fee"));
            //LogHelper.Log.Write("appid:" + data.GetValue("appid"));

            //LogHelper.Log.Write("notify_url:" + data.GetValue("notify_url"));

            string xml = data.ToXml();
            string response = WxPayAPI.HttpService.Post(xml, url, false, 5);

            WxPayAPI.WxPayData result = new WxPayAPI.WxPayData();
            result.FromXml(response);

            WxPayAPI.WxPayData jsApiParam = new WxPayAPI.WxPayData();
            jsApiParam.SetValue("appId", result.GetValue("appid"));
            jsApiParam.SetValue("timeStamp", WxPayAPI.WxPayApi.GenerateTimeStamp());
            jsApiParam.SetValue("nonceStr", WxPayAPI.WxPayApi.GenerateNonceStr());
            jsApiParam.SetValue("package", "prepay_id=" + result.GetValue("prepay_id"));
            jsApiParam.SetValue("signType", "MD5");
            jsApiParam.SetValue("paySign", jsApiParam.MakeSign());

            string jsonParam = jsApiParam.ToJson();
            ViewData["Result"] = result;
            ViewData["JsonResult"] = jsonParam;

            return View();
        }

        [HttpPost]
        public void RechargePayResult()
        {
            //接收从微信后台POST过来的数据
            System.IO.Stream s = Request.InputStream;
            int count = 0;
            byte[] buffer = new byte[1024];
            StringBuilder builder = new StringBuilder();
            while ((count = s.Read(buffer, 0, 1024)) > 0)
            {
                builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
            }
            s.Flush();
            s.Close();
            s.Dispose();

            //转换数据格式并验证签名
            WxPayData data = new WxPayData();
            try
            {
                data.FromXml(builder.ToString());

                Member member = _memberBLL.GetMemberByOpenId(data.GetValue("openid").ToString());

                #region 赠送的积分计算

                // 充值赠送的积分  
                // 目前数值待定  怎么获取待定

                double rechargeCredit = GetRechargeCreditAndFree(double.Parse(data.GetValue("total_fee").ToString())/100);
                
                #endregion

                // 没有余额了 
                // 充值都充到积分去 
                member.Credit += rechargeCredit;
                member.TotalCredit += rechargeCredit;

                if (_memberBLL.Update(member))
                {
                    Recharge recharge = new Recharge();
                    recharge.Id = Guid.NewGuid();
                    recharge.MemberId = member.Id;
                    recharge.Amount = rechargeCredit;
                    recharge.CreatedTime = DateTime.Now;
                    recharge.DeletedTime = DateTime.MinValue.AddHours(8);
                    recharge.IsDeleted = false;
                    recharge.Payway = 0;

                    if (_recharegeBLL.Add(recharge))
                    {
                        LogHelper.Log.Write("充值成功");
                        // 添加充值积分记录

                        if (AddRechargeCredit(member, rechargeCredit))
                        {
                            // 异步判断是否够积分升级vip
                            UpGradeDel del = new UpGradeDel(UpGradeVIP);
                            IAsyncResult ar = del.BeginInvoke(member.Id, CallBackMethod, null);
                        }
                    }
                    else
                    {
                        LogHelper.Log.Write("充值失败");
                    }
                }
                else
                {
                    LogHelper.Log.Write("更新余额失败");
                }
            }
            catch (WxPayException ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                //若签名错误，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", ex.Message);
                Log.Error(this.GetType().ToString(), "Sign check error : " + res.ToXml());
                Response.Write(res.ToXml());
                Response.End();
            }

            Log.Info(this.GetType().ToString(), "Check sign success");

            WxPayData successData = new WxPayData();
            successData.SetValue("return_code", "SUCCESS");
            successData.SetValue("return_msg", "OK");

            Response.Write(successData.ToXml());
            Response.End();
        }

        public double GetRechargeCreditAndFree(double total_fee)
        {
            switch ((int)total_fee)
            {
                case 5000:
                    return 5000 * 10 % +5000;
                case 10000:
                    return 10000 * 12 % +10000;
                case 15000:
                    return 15000 * 14 % +15000;
                case 20000:
                    return 20000 * 16 % +20000;
                case 25000:
                    return 25000 * 18 % +25000;
                case 30000:
                    return 30000 * 20 % +30000;
                default:
                    break;
            }
            return 0;
        }

        public ActionResult ConsumeService(Guid serviceId)
        {
            if (serviceId == null)
            {
                return RedirectToAction("Error");
            }

            MyService ms = _serviceBLL.GetMyServiceByServiceId(serviceId);

            if (ms == null)
            {
                return RedirectToAction("Error");
            }

            ServiceModel sm = new ServiceModel();
            sm.MemberName = _memberBLL.GetMemberById(ms.MemberId).NickeName;
            sm.ServiceId = ms.Id;
            sm.ServiceName = ms.GoodsName;

            return View(sm);
        }

        [HttpPost]
        public ActionResult ConsumeService(Guid serviceId, string password)
        {
            if (serviceId == null)
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(password))
            {
                return Json("PasswordError", JsonRequestBehavior.AllowGet);
            }
            // 获取消费密码
            ServiceConsumePassword scp = _serviceConsumePasswordBLL.GetServicePassword();
            if (scp.Password == password)
            {
                ServiceConsumeRecord scr = new ServiceConsumeRecord();
                scr.Id = Guid.NewGuid();
                scr.IsDeleted = false;
                scr.ServiceId = serviceId;
                scr.CreatedTime = DateTime.Now;
                scr.DeletedTime = DateTime.MinValue.AddHours(8);

                if (_serviceConsumeRecoredBLL.Add(scr))
                {
                    MyService ms = _serviceBLL.GetMyServiceByServiceId(serviceId);
                    if (ms.CurrentCount > 0)
                    {
                        ms.CurrentCount -= 1;
                    }
                    else
                    {
                        return Json("False", JsonRequestBehavior.AllowGet);
                    }
                    _serviceBLL.Update(ms);

                    return Json("True", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("False", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("PasswordError", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult MyServiceQR(Guid serviceId)
        {
            ServiceQR qr = _serviceQRBLL.GetServiceQRByServcieId(serviceId);
            MyService ms = _serviceBLL.GetMyServiceByServiceId(serviceId);

            ViewBag.Service = ms;

            return View(qr);
        }

        public ActionResult MyExchangeServiceQR(Guid ExchangeServiceRecordId)
        {
            ExchangeServiceQR qr = _exchangeServiceQRBLL.GetExchangeServiceQRById(ExchangeServiceRecordId);

            ExchangeServiceRecord esr = _exchangeServiceRecordBLL.GetESRById(ExchangeServiceRecordId);

            ViewBag.ServiceName = _exchangeServiceBLL.GetNoDeletedExchangeServiceById(esr.ExchangeSerivceId).Name;
            ViewBag.CreateTime = esr.CreatedTime;

            return View(qr);
        }

        public ActionResult ConsumeExchangeService(Guid esrId)
        {
            if (esrId == null)
            {
                return RedirectToAction("Error");
            }
            ExchangeServiceRecord esr = _exchangeServiceRecordBLL.GetESRById(esrId);
            ExchangeServiceModel esm = new ExchangeServiceModel();

            esm.MemberName = _memberBLL.GetMemberById(esr.MemberId).NickeName;
            esm.ExchangeServiceId = esr.Id;
            esm.ExchangeServiceName = _exchangeServiceBLL.GetNoDeletedExchangeServiceById(esr.ExchangeSerivceId).Name;

            return View(esm);
        }

        [HttpPost]
        public ActionResult ConsumeExchangeService(Guid esrId, string password)
        {
            if (esrId == null)
            {
                return Json("True", JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(password))
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            // 获取消费密码
            ServiceConsumePassword scp = _serviceConsumePasswordBLL.GetServicePassword();
            if (scp.Password == password)
            {
                ExchangeServiceRecord esr = _exchangeServiceRecordBLL.GetESRById(esrId);
                esr.IsUse = true;

                if (_exchangeServiceRecordBLL.Update(esr))
                {
                    return Json("True", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("False", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 领取优惠券页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ReceiveCoupon()
        {
            return View();
        }

        /// <summary>
        /// 领取方法
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        [IsRegister("/Pay/ReceiveCoupon")]
        public ActionResult Receive(string code)
        {
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"] as String);

            if (member == null)
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            Goods good = new Goods();
            good = _goodsBLL.GetGoodsByCode(code);

            if (good == null)
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            MyService myService = new MyService();
            myService.Id = Guid.NewGuid();
            myService.GoodsName = good.Name;
            myService.GoodsId = good.Id;
            myService.CreatedTime = DateTime.Now;
            myService.CurrentCount = good.ServiceCount;
            myService.DeletedTime = DateTime.MinValue.AddHours(8);
            myService.IsDeleted = false;
            myService.MemberId = member.Id;
            myService.TotalCount = good.ServiceCount;

            if (!_serviceBLL.Add(myService))
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
            else
            {
                CreateServiceQR(member.Id, myService.Id);
                return Json("True", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 添加充值积分记录
        /// </summary>
        /// <param name="member"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        [NonAction]
        private bool AddRechargeCredit(Member member, double money)
        {
            CreditRecord cr = new CreditRecord();
            cr.Id = Guid.NewGuid();
            cr.MemberId = member.Id;
            cr.IsDeleted = false;           
            cr.Money = money;
            cr.OperationType = "Recharge";
            cr.CreatedTime = DateTime.Now;
            cr.CurrentCreditCoefficient = double.Parse(ConfigurationManager.AppSettings["Recharge"].ToString());
            cr.DeletedTime = DateTime.MinValue.AddHours(8);
            cr.Notes = "充值送积分,获得" + cr.Money + "积分";

            //if (_creditRecordBLL.Add(cr))
            //{
            //    member.Credit += cr.Money * cr.CurrentCreditCoefficient; // 当前积分
            //    member.TotalCredit += cr.Money * cr.CurrentCreditCoefficient; // 累计总积分
            //    _memberBLL.Update(member);

            //    return true;
            //}
            if (_creditRecordBLL.Add(cr))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 添加消费积分记录
        /// </summary>
        /// <param name="member"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        [NonAction]
        private bool AddConsumeCredit(Member member, double money, bool IsComeIn)
        {
            CreditRecord cr = new CreditRecord();
            cr.Id = Guid.NewGuid();
            cr.CreatedTime = DateTime.Now;
            cr.DeletedTime = DateTime.MinValue.AddHours(8);
            cr.IsDeleted = false;
            cr.MemberId = member.Id;

            // 这里 money 不是钱 是积分 
            cr.Money = money;

            // 判断是收益还是消费
            if (!IsComeIn)
            {
                // 如果是消费
                cr.OperationType = "Consumption";
                cr.Notes = "积分消费，使用了" + cr.Money + "积分";

                if (_creditRecordBLL.Add(cr))
                {
                    member.Credit -= cr.Money;
                    _memberBLL.Update(member);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                // 如果是微信支付的赠送积分
                cr.CurrentCreditCoefficient = double.Parse(ConfigurationManager.AppSettings["Free"].ToString());
                cr.OperationType = "Free";
                cr.Notes = "微信付款赠送积分:获得" + cr.Money * cr.CurrentCreditCoefficient + "积分";

                if (_creditRecordBLL.Add(cr))
                {
                    member.Credit += cr.Money * cr.CurrentCreditCoefficient;
                    member.TotalCredit += cr.Money * cr.CurrentCreditCoefficient;

                    _memberBLL.Update(member);

                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        /// <summary>
        /// 升级vip函数  先判断是否够积分升级vip 如果够则直接升级vip
        /// </summary>
        /// <param name="memberId"></param>
        [NonAction]
        private void UpGradeVIP(Guid memberId)
        {
            Member member = _memberBLL.GetMemberById(memberId);
            int targetVIP;
            if (_rulesBLL.UpGradeVIP(member.TotalCredit, member.Vip, out targetVIP))
            {
                member.Vip = targetVIP;
                _memberBLL.Update(member);
            }
        }

        /// <summary>
        /// UpGradeDel 的回调方法
        /// </summary>
        /// <param name="ar"></param>
        [NonAction]
        private void CallBackMethod(IAsyncResult ar)
        {
            try
            {
                UpGradeDel del = (UpGradeDel)ar.AsyncState;
                del.EndInvoke(ar);
            }
            catch (Exception)
            {
            }

        }

        /// <summary>
        ///  添加我的服务
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="OrderNo"></param>
        [NonAction]
        private void AddMyService(Guid memberId, string OrderNo)
        {
            // 先根据orderNo 获取到所有的 OrderDetail 里面的商品列表

            List<OrderDetail> odList = _orderDetailBLL.GetOrderDetailByOrderNo(OrderNo).ToList();
            List<Goods> serviceList = new List<Goods>();

            // 再判断 servicecount 是否大于 0 来找出服务对象
            foreach (var item in odList)
            {
                Goods goods = _goodsBLL.GetGoodsById(item.GoodsId);
                if (goods.ServiceCount > 0)
                {
                    serviceList.Add(goods);
                }
            }
            // 最后根据所得服务列表  添加 MyService 表
            foreach (var item in serviceList)
            {
                MyService ms = new MyService();
                ms.Id = Guid.NewGuid();
                ms.IsDeleted = false;
                ms.MemberId = memberId;
                ms.TotalCount = item.ServiceCount;
                ms.CurrentCount = item.ServiceCount;
                ms.CreatedTime = DateTime.Now;
                ms.DeletedTime = DateTime.MinValue.AddHours(8);
                ms.GoodsId = item.Id;
                ms.GoodsName = item.Name;

                _serviceBLL.Add(ms);

                // 生成二维码
                CreateServiceQR(memberId, ms.Id);
            }
        }

        /// <summary>
        /// 添加我的服务的回调方法
        /// </summary>
        /// <param name="ar"></param>
        [NonAction]
        private void MyServiceCallBackMethod(IAsyncResult ar)
        {
            try
            {
                AddMyServiceDel del = (AddMyServiceDel)ar.AsyncState;
                del.EndInvoke(ar);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 生成二维码 并添加路径
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="serviceId"></param>
        [NonAction]
        private void CreateServiceQR(Guid memberId, Guid serviceId)
        {
            try
            {
                string dir = Server.MapPath("~/QR/");

                ServiceQR sqr = new ServiceQR();
                sqr.Id = Guid.NewGuid();

                sqr.IsDeleted = false;
                sqr.MemberId = memberId;
                sqr.ServcieId = serviceId;
                sqr.CreatedTime = DateTime.Now;
                sqr.DeletedTime = DateTime.MinValue.AddHours(8);

                string sourceString = "http://jcb.ybtx88.com/Pay/ConsumeService?serviceId=" + serviceId.ToString();

                LogHelper.Log.Write("sourceString: " + sourceString);

                string qrPath = QRCodeCreator.Create(sourceString, dir);

                sqr.QRPath = qrPath;

                _serviceQRBLL.Add(sqr);
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