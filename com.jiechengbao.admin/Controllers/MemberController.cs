using com.jiechengbao.Ibll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.admin.Controllers
{
    public class MemberController:Controller
    {
        private IMemberBLL _memberBLL;
        public MemberController(IMemberBLL memberBLL)
        {
            _memberBLL = memberBLL;
        }
        public ActionResult List()
        {
            ViewData["MemberList"] = _memberBLL.GetAllNoDeletedMembers();
            return View();
        }
    }
}