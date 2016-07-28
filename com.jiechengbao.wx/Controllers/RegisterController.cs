using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using com.jiechengbao.wx.Global;
using com.jiechengbao.wx.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.wx.Controllers
{
    public class RegisterController : Controller
    {
        private IMemberBLL _memberBLL;
        public RegisterController(IMemberBLL memberBLL)
        {
            _memberBLL = memberBLL;
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult GetCode(string phone)
        {
            string msg = ConfigurationManager.AppSettings["Msg"].ToString();
            PhoneCode pc = new PhoneCode();

            if (pc.GetCode(phone, msg))
            {
                return Json("True", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            // 判断参数是否完整
            if (model == null)
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            // 判断验证码是否正确
            if (model.code != System.Web.HttpContext.Current.Session["CertCode"].ToString())
            {
                return Json("CodeError", JsonRequestBehavior.AllowGet);
            }
            // 判断姓名是否为空
            if (string.IsNullOrEmpty(model.realName))
            {
                return Json("NameError", JsonRequestBehavior.AllowGet);
            }

            // 更新会员信息
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());

            member.RealName = model.realName;
            member.Phone = model.phone;

            if (_memberBLL.Update(member))
            {
                // 绑定成功
                return Json("True", JsonRequestBehavior.AllowGet);
            }
            else
            {
                // 绑定失败
                return Json("False", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult IsRegister()
        {
            Member mem = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());

            if (string.IsNullOrEmpty(mem.Phone))
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("True", JsonRequestBehavior.AllowGet);
            }
        }
    }
}