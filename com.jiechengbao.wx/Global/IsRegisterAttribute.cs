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
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            IMemberDAL dal = new MemberDAL();
            IMemberBLL _memberBLL = new MemberBLL(dal);

            Member mem = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());

            if (string.IsNullOrEmpty(mem.Phone))
            { 
                filterContext.Result = new RedirectResult("/Register/Register");
            }
        }
    }
}