using com.jiechengbao.admin.Models;
using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.admin.Controllers
{
    public class MemberController:Controller
    {
        private IMemberBLL _memberBLL;
        private IRulesBLL _rulesBLL;
        public MemberController(IMemberBLL memberBLL, IRulesBLL rulesBLL)
        {
            _memberBLL = memberBLL;
            _rulesBLL = rulesBLL;
        }

        public ActionResult List()
        {
            ViewData["MemberList"] = _memberBLL.GetAllNoDeletedMembers();
            return View();
        }

        public ActionResult VIPSetting(List<VIPModel> vipList)
        {
            if (Request.HttpMethod.ToLower() == "post")
            {
                Stream s = System.Web.HttpContext.Current.Request.InputStream;
                byte[] b = new byte[s.Length];
                s.Read(b, 0, (int)s.Length);
                var post = Encoding.UTF8.GetString(b);

                if (!string.IsNullOrEmpty(post))
                {
                    Response.Write(post);
                }

            }

            List<Rules> rulesList = _rulesBLL.GetAllRules().OrderBy(n => n.VIP).ToList();

            ViewData["RulesList"] = rulesList;
            return View();
        }
    }
}