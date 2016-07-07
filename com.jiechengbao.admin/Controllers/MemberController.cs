using com.jiechengbao.admin.Models;
using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.admin.Controllers
{
    public class MemberController : Controller
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

        [HttpPost]
        public ActionResult List(string condition)
        {
            if (string.IsNullOrEmpty(condition))
            {
                return RedirectToAction("List");
            }

            Member member = _memberBLL.GetMembersByNickNameAndPhone(condition);

            ViewBag.Member = member;

            return View();
        }

        public ActionResult VIPSetting()
        {
            // 因为系统会默认的在 Rules 表中添加VIP0的记录 但是又不需要显示出来 所以查询的时候就要忽略掉第0个
            List<Rules> rulesList = _rulesBLL.GetAllRules().OrderBy(n => n.VIP).Skip(0).ToList();

            ViewData["RulesList"] = rulesList;
            return View();
        }

        [HttpPost]
        public ActionResult SaveVIPSetting()
        {
            if (Request.IsAjaxRequest())
            {
                var stream = HttpContext.Request.InputStream;
                string json = new StreamReader(stream).ReadToEnd();

                try
                {
                    // 先获取当前最高vip等级
                    int currentVIPCount = _rulesBLL.GetAllRules().Count() - 1;

                    List<Rules> rulesList = new List<Rules>();

                    JObject jarray = (JObject)JsonConvert.DeserializeObject(json);

                    if (jarray["hehe"].Count() < currentVIPCount)
                    {
                        // 设置vip规则的时候 要注意 新设置的vip数量不能少于之前的vip数量

                        // 这也是为了保证数据的完整性

                        return Json("CountError", JsonRequestBehavior.AllowGet);
                    }

                    foreach (var item in jarray["hehe"])
                    {
                        Rules rules = new Rules();
                        rules.Id = Guid.NewGuid();
                        rules.CreatedTime = DateTime.Now.Date;
                        rules.DeletedTime = DateTime.MinValue.AddHours(8);
                        rules.Discount = double.Parse(item["Discount"].ToString());
                        rules.IsDeleted = false;
                        rules.TotalCredit = double.Parse(item["TotalCredit"].ToString());
                        rules.VIP = int.Parse(item["VIP"].ToString());

                        rulesList.Add(rules);
                    }

                    Rules rules0 = new Rules();
                    rules0.Id = Guid.NewGuid();
                    rules0.IsDeleted = false;
                    rules0.TotalCredit = 0;
                    rules0.VIP = 0;
                    rules0.Discount = 1;
                    rules0.DeletedTime = DateTime.MinValue.AddHours(8);
                    rules0.CreatedTime = DateTime.Now.Date;

                   

                    if (_rulesBLL.Clear())
                    {
                        if (_rulesBLL.Add(rulesList))
                        {
                            _rulesBLL.Add(rules0);
                            return Json("True", JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json("False", JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json("Error", JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Log.Write(ex.Message);
                    LogHelper.Log.Write(ex.StackTrace);
                    throw;
                }
            }
            else
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
        }

        [NonAction]
        public static Object JsonToObj(String json, Type t)
        {
            try
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(t);
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                {
                    return serializer.ReadObject(ms);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}