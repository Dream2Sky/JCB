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

            if (System.Web.HttpContext.Current.Session["IsRegister"] == null)
            {
                Member mem = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());

                if (string.IsNullOrEmpty(mem.RealName) && string.IsNullOrEmpty(mem.Phone))
                {
                    System.Web.HttpContext.Current.Session["IsRegister"] = "false";
                    filterContext.Result = new RedirectResult("/Register/Register");
                }
                else
                {
                    System.Web.HttpContext.Current.Session["IsRegister"] = "true";
                }
            }
            else
            {
                if (System.Web.HttpContext.Current.Session["IsRegister"].ToString() == "false")
                {
                    filterContext.Result = new RedirectResult("/Register/Register");
                }
            }
        }
    }
}