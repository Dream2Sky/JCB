using com.jiechengbao.common;
using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using com.jiechengbao.wx.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.wx.Controllers
{
    public class OauthController : Controller
    {
        private IMemberBLL _memberBLL;
        public OauthController(IMemberBLL memberBLL)
        {
            _memberBLL = memberBLL;
        }

        /// <summary>
        /// 商城入口
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult GetCode(string code)
        {
            LogHelper.Log.Write("传递的code:" + code);
            if (string.IsNullOrEmpty(code))
            {
                string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + WxConfig.AppId + "&redirect_uri=http://" + WxConfig.WxDomain + "/Oauth/GetCode&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect";
                System.Web.HttpContext.Current.Response.Redirect(url);
                return RedirectToAction("GetCode");
            }
            else
            {
                CacheManager.SetCache("code", code);
            }
            UserInfo_JsonModel user = GetWxUserInfo();

            LogHelper.Log.Write("openid:" + user.openid + ", nickname:" + user.nickname + ", sex:" + user.sex + ", headerimage:" + user.headimgurl);

            if (!_memberBLL.IsExist(user.openid))
            {
                Member member = new Member();
                member.Id = Guid.NewGuid();
                member.IsDeleted = false;
                member.NickeName = user.nickname;
                member.OpenId = user.openid;
                member.Vip = 0;
                member.HeadImage = user.headimgurl;
                //member.Assets = 0;
                member.CreatedTime = DateTime.Now;
                member.Credit = 0;
                member.DeletedTime = DateTime.MinValue.AddHours(8);
                member.RealName = "";
                member.TotalCredit = 0;
                member.Phone = "";

                if (!_memberBLL.Add(member))
                {
                    LogHelper.Log.Write("添加新用户失败");
                }
            }
            System.Web.HttpContext.Current.Session["member"] = user.openid;

            if (Request.UrlReferrer == null || Request.UrlReferrer.Host != Request.Url.Host)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
        }

        /// <summary>
        /// 一键救援入口
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult GetHelpCode(string code)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            if (string.IsNullOrEmpty(code))
            {
                string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + WxConfig.AppId + "&redirect_uri=http://" + WxConfig.WxDomain + "/Oauth/GetHelpCode&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect";

                System.Web.HttpContext.Current.Response.Redirect(url);
                sw.Stop();
                LogHelper.Log.Write("已经存在code 直接跳转所花费的时间为: " + sw.ElapsedMilliseconds);
                return RedirectToAction("GetHelpCode");
            }
            else
            {
                CacheManager.SetCache("code", code);
            }
            UserInfo_JsonModel user = GetWxUserInfo();
            if (!_memberBLL.IsExist(user.openid))
            {
                Member member = new Member();
                member.Id = Guid.NewGuid();
                member.IsDeleted = false;
                member.NickeName = user.nickname;
                member.OpenId = user.openid;
                member.Vip = 0;
                member.HeadImage = user.headimgurl;
                //member.Assets = 0;
                member.CreatedTime = DateTime.Now;
                member.Credit = 0;
                member.DeletedTime = DateTime.MinValue.AddHours(8);
                member.RealName = "";
                member.IsDeleted = false;
                member.TotalCredit = 0;
                member.Phone = "";
                if (!_memberBLL.Add(member))
                {
                    LogHelper.Log.Write("添加新用户失败");
                }

            }

            System.Web.HttpContext.Current.Session["member"] = user.openid;

            sw.Stop();
            LogHelper.Log.Write("重新获取code 再跳转所花费的时间为: " + sw.ElapsedMilliseconds);
            if (Request.UrlReferrer == null || Request.UrlReferrer.Host != Request.Url.Host)
            {
                return RedirectToAction("Help", "UserInfo");
            }
            else
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
        }

        /// <summary>
        /// 会员充值入口
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult GetRechargeCode(string code)
        {
            LogHelper.Log.Write("传递的code:" + code);
            if (string.IsNullOrEmpty(code))
            {
                string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + WxConfig.AppId + "&redirect_uri=http://" + WxConfig.WxDomain + "/Oauth/GetRechargeCode&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect";

                System.Web.HttpContext.Current.Response.Redirect(url);

                return RedirectToAction("GetRechargeCode");
            }
            else
            {
                CacheManager.SetCache("code", code);
            }
            UserInfo_JsonModel user = GetWxUserInfo();

            LogHelper.Log.Write("openid:" + user.openid + ", nickname:" + user.nickname + ", sex:" + user.sex + ", headerimage:" + user.headimgurl);

            if (!_memberBLL.IsExist(user.openid))
            {
                Member member = new Member();
                member.Id = Guid.NewGuid();
                member.IsDeleted = false;
                member.NickeName = user.nickname;
                member.OpenId = user.openid;
                member.Vip = 0;
                member.HeadImage = user.headimgurl;
                //member.Assets = 0;
                member.CreatedTime = DateTime.Now;
                member.Credit = 0;
                member.DeletedTime = DateTime.MinValue.AddHours(8);
                member.RealName = "";
                member.IsDeleted = false;
                member.TotalCredit = 0;
                member.Phone = "";
                if (!_memberBLL.Add(member))
                {
                    LogHelper.Log.Write("添加新用户失败");
                }

            }
            LogHelper.Log.Write("user's openid = " + user.openid);

            System.Web.HttpContext.Current.Session["member"] = user.openid;

            if (Request.UrlReferrer == null || Request.UrlReferrer.Host != Request.Url.Host)
            {
                return RedirectToAction("RechargeOptionsList", "UserInfo");
            }
            else
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
        }

        /// <summary>
        /// 个人中心入口
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult GetUserInfoCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + WxConfig.AppId + "&redirect_uri=http://" + WxConfig.WxDomain + "/Oauth/GetUserInfoCode&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect";

                System.Web.HttpContext.Current.Response.Redirect(url);

                return RedirectToAction("GetRechargeCode");
            }
            else
            {
                CacheManager.SetCache("code", code);
            }
            UserInfo_JsonModel user = GetWxUserInfo();

            if (!_memberBLL.IsExist(user.openid))
            {
                Member member = new Member();
                member.Id = Guid.NewGuid();
                member.IsDeleted = false;
                member.NickeName = user.nickname;
                member.OpenId = user.openid;
                member.Vip = 0;
                member.HeadImage = user.headimgurl;
                //member.Assets = 0;
                member.CreatedTime = DateTime.Now;
                member.Credit = 0;
                member.DeletedTime = DateTime.MinValue.AddHours(8);
                member.RealName = "";
                member.IsDeleted = false;
                member.TotalCredit = 0;
                member.Phone = "";

                if (!_memberBLL.Add(member))
                {
                    LogHelper.Log.Write("添加新用户失败");
                }

            }
            System.Web.HttpContext.Current.Session["member"] = user.openid;

            if (Request.UrlReferrer == null || Request.UrlReferrer.Host != Request.Url.Host)
            {
                return RedirectToAction("Info", "UserInfo");
            }
            else
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
        }

        /// <summary>
        /// 在线预约入口
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult GetAppointmentCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + WxConfig.AppId + "&redirect_uri=http://" + WxConfig.WxDomain + "/Oauth/GetAppointmentCode&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect";

                System.Web.HttpContext.Current.Response.Redirect(url);

                return RedirectToAction("GetRechargeCode");
            }
            else
            {
                CacheManager.SetCache("code", code);
            }
            UserInfo_JsonModel user = GetWxUserInfo();

            if (!_memberBLL.IsExist(user.openid))
            {
                Member member = new Member();
                member.Id = Guid.NewGuid();
                member.IsDeleted = false;
                member.NickeName = user.nickname;
                member.OpenId = user.openid;
                member.Vip = 0;
                member.HeadImage = user.headimgurl;
                //member.Assets = 0;
                member.CreatedTime = DateTime.Now;
                member.Credit = 0;
                member.DeletedTime = DateTime.MinValue.AddHours(8);
                member.RealName = "";
                member.IsDeleted = false;
                member.TotalCredit = 0;
                member.Phone = "";

                LogHelper.Log.Write(member.Id.ToString());
                LogHelper.Log.Write(member.IsDeleted.ToString());
                LogHelper.Log.Write(member.NickeName);
                LogHelper.Log.Write(member.OpenId);
                LogHelper.Log.Write(member.Vip.ToString());
                LogHelper.Log.Write(member.HeadImage);
                LogHelper.Log.Write(member.CreatedTime.ToString());
                LogHelper.Log.Write(member.Credit.ToString());
                LogHelper.Log.Write(member.DeletedTime.ToString());
                LogHelper.Log.Write(member.RealName);
                LogHelper.Log.Write(member.IsDeleted.ToString());
                LogHelper.Log.Write(member.TotalCredit.ToString());
                LogHelper.Log.Write(member.Phone);


                if (!_memberBLL.Add(member))
                {
                    LogHelper.Log.Write("添加新用户失败");
                }

            }
            System.Web.HttpContext.Current.Session["member"] = user.openid;

            if (Request.UrlReferrer == null || Request.UrlReferrer.Host != Request.Url.Host)
            {
                return RedirectToAction("MyAppointment", "Appointment");
            }
            else
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
        }

        /// <summary>
        /// 优惠券领取入口
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult GetCouponCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                string url = @"https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + WxConfig.AppId + "&redirect_uri=http://" + WxConfig.WxDomain + "/Oauth/GetCouponCode&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect";

                System.Web.HttpContext.Current.Response.Redirect(url);

                return RedirectToAction("GetCouponCode");
            }
            else
            {
                CacheManager.SetCache("code", code);
            }
            UserInfo_JsonModel user = GetWxUserInfo();

            if (!_memberBLL.IsExist(user.openid))
            {
                Member member = new Member();
                member.Id = Guid.NewGuid();
                member.IsDeleted = false;
                member.NickeName = user.nickname;
                member.OpenId = user.openid;
                member.Vip = 0;
                member.HeadImage = user.headimgurl;
                //member.Assets = 0;
                member.CreatedTime = DateTime.Now;
                member.Credit = 0;
                member.DeletedTime = DateTime.MinValue.AddHours(8);
                member.RealName = "";
                member.IsDeleted = false;
                member.TotalCredit = 0;
                member.Phone = "";

                if (!_memberBLL.Add(member))
                {
                    LogHelper.Log.Write("添加新用户失败");
                }
            }
            System.Web.HttpContext.Current.Session["member"] = user.openid;

            if (Request.UrlReferrer == null || Request.UrlReferrer.Host != Request.Url.Host)
            {
                return RedirectToAction("CouponList", "FreeCoupon");
            }
            else
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
        }

        private UserInfo_JsonModel GetWxUserInfo()
        {
            string code = CacheManager.GetCache("code").ToString();
            string responseString = string.Empty;

            string url = string.Format(@"https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + WxConfig.AppId + "&secret=" + WxConfig.AppSecret + "&code={0}&grant_type=authorization_code", code);

            responseString = HttpManager.AccessURL_GET(url);
            Access_Token_JsonModel model = new Access_Token_JsonModel();
            UserInfo_JsonModel user = new UserInfo_JsonModel();

            try
            {
                model = JsonConvert.DeserializeObject<Access_Token_JsonModel>(responseString);

                LogHelper.Log.Write("access_token:" + model.access_token);

                user = GetUserInfo(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }

            return user;
        }

        /// <summary>
        /// 通过access_token获取用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private UserInfo_JsonModel GetUserInfo(Access_Token_JsonModel model)
        {
            string responseString = string.Empty;
            string url = string.Format(@"https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}", model.access_token, model.openid);

            responseString = HttpManager.AccessURL_GET(url);
            LogHelper.Log.Write("打印微信返回的xml");
            LogHelper.Log.Write(responseString);
            UserInfo_JsonModel user = new UserInfo_JsonModel();
            try
            {
                user = JsonConvert.DeserializeObject<UserInfo_JsonModel>(responseString);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }

            return user;
        }
    }
}