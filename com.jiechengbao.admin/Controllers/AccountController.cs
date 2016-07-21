using com.jiechengbao.admin.Models;
using com.jiechengbao.entity;
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
                return RedirectToAction("Login", new { msg = "账号或密码错误" });
            }
        }

        public ActionResult Logout()
        {
            if (System.Web.HttpContext.Current.Session["admin"] != null)
            {
                System.Web.HttpContext.Current.Session["admin"] = null;
            }
            return RedirectToAction("Login");
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(string oldpassword,string newpassword)
        {
            Admin admin = _adminBLL.GetAdminByAccount(System.Web.HttpContext.Current.Session["admin"].ToString());
            if (true)
            {
                // 这个方法待定
            }

            return View();
        }
    }
}