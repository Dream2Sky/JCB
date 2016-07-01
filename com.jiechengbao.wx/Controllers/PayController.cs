
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

namespace com.jiechengbao.wx.Controllers
{
    
    public class PayController:Controller
    {
        private IMemberBLL _memberBLL;
        private IOrderBLL _orderBLL;
        private ITransactionBLL _transactionBLL;
        public PayController(IMemberBLL memberBLL, IOrderBLL orderBLL ,ITransactionBLL transactionBLL)
        {
            _memberBLL = memberBLL;
            _orderBLL = orderBLL;
            _transactionBLL = transactionBLL;
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
            orm.total_fee = double.Parse(Request.QueryString["totalprice"].ToString());
            orm.trade_type = "JSAPI";
            orm.spbill_create_ip = Request.QueryString["ip"].ToString();
            orm.out_trade_no = Request.QueryString["orderNo"].ToString();
            orm.appid = WxPayAPI.WxPayConfig.APPID;
            orm.body = "捷诚宝商城";
            orm.mch_id = WxPayAPI.WxPayConfig.MCHID;
            orm.nonce_str = WxPayAPI.WxPayApi.GenerateNonceStr();
            orm.notify_url = HttpContext.Request.Url.Scheme+"://"+HttpContext.Request.Url.Host+":"+HttpContext.Request.Url.Port+"/WxPay/PayResult";

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
                var res = new {
                    isSuccess = false,
                    msg = "提交的参数有误"
                };
                return Json(res, JsonRequestBehavior.AllowGet);
            }

            #region 更新订单状态

            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());

            // 取得 order对象 
            Order order = _orderBLL.GetOrderByOrderNo(orderNo);

            #region 判断余额是否充值
            // 判断余额 是否 充值
            if (member.Assets < order.TotalPrice)
            {
                var res = new
                {
                    isSuccess = false,
                    msg = "余额不足"
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

            // 修改order对象 payway = ye ye表示余额支付
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

                // 修改账户余额
                member.Assets = member.Assets - order.TotalPrice;
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

            var json = new {
                isSuccess = true,
                msg = "支付成功"
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}