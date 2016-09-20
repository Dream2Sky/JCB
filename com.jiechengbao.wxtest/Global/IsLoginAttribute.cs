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
            if (System.Web.HttpContext.Current.Session["member"] == null)
            {
                filterContext.Result = new RedirectResult("/Oauth/GetCode");
            }
        }
    }
}