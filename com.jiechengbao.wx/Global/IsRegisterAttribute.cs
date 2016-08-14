using com.jiechengbao.bll;
using com.jiechengbao.dal;
using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.wx.Global
{
    public class IsRegisterAttribute : ActionFilterAttribute
    {
        private string refUrl = string.Empty;
        public IsRegisterAttribute(string refurl)
        {
            this.refUrl = refurl;
        }

        public IsRegisterAttribute()
        { }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            IMemberDAL dal = new MemberDAL();
            MemberBLL _memberBLL = new MemberBLL(dal);

            Member mem = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());
            
            if (string.IsNullOrEmpty(mem.Phone))
            {
                if (this.refUrl == string.Empty)
                {
                    filterContext.Result = new RedirectResult("/Register/Register");
                }
                else
                {
                    filterContext.Result = new RedirectResult("/Register/Register?refurl=" + refUrl);
                }
            }
        }
    }
}