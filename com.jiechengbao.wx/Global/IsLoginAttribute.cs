using com.jiechengbao.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.wx.Global
{
    public class IsLoginAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            LogHelper.Log.Write("Now starting recording");
            LogHelper.Log.Write("member session is null?");
            if (System.Web.HttpContext.Current.Session["member"] == null)
            {
                //LogHelper.Log.Write("member session is null");
                //string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx3fab45769c82a189&redirect_uri=http://jcb.ybtx88.com/Oauth/GetCode&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect";

                //System.Web.HttpContext.Current.Response.Redirect(url);

                filterContext.Result = new RedirectResult("/Oauth/GetCode");
            }
        }
    }
}