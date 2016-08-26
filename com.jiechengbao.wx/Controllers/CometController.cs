using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.wx.Controllers
{
    public class CometController:Controller
    {
        public ActionResult IsConsume()
        {
            if (System.Web.HttpContext.Current.Session["IsPay"] != null)
            {
                System.Web.HttpContext.Current.Session["IsPay"] = null;
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public void CreateSession()
        {
            System.Web.HttpContext.Current.Session["IsPay"] = "True";
        }
    }
}