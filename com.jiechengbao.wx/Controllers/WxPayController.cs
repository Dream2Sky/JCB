using com.jiechengbao.wx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.wx.Controllers
{
    public class WxPayController:Controller
    {
        public ActionResult Pay()
        {
            string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";

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

            LogHelper.Log.Write("openid:" + data.GetValue("openid"));
            LogHelper.Log.Write("total_fee:" + data.GetValue("total_fee"));
            LogHelper.Log.Write("appid:" + data.GetValue("appid"));

            LogHelper.Log.Write("notify_url:" + data.GetValue("notify_url"));

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

    }
}