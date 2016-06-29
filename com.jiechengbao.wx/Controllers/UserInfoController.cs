using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.wx.Controllers
{
    public class UserInfoController:Controller
    {
        public ActionResult Index()
        {
            System.Web.HttpContext.Current.Session["member"] = "okzkZv6LHCo-vIyZHynDoXjeUbKs";
            return View();
        }

        public ActionResult Help()
        {
            return View();
        }

        public ActionResult Info()
        {
            return View();
        }
    }
}