using com.jiechengbao.admin.Models;
using com.jiechengbao.Ibll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.admin.Controllers
{
    public class AccountController:Controller
    {
        private IAdminBLL _adminBLL;
        public AccountController(IAdminBLL adminBLL)
        {
            _adminBLL = adminBLL;
        }

        public ActionResult Login(bool? msg)
        {
            ViewBag.Msg = msg;
            return View();
        }

        [HttpPost]
        public ActionResult Login(AccountModel model)
        {
            if (_adminBLL.Login(model.account,model.password))
            {
                System.Web.HttpContext.Current.Session["admin"] = model.account;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", new { msg = false });
            }
        }
    }
}