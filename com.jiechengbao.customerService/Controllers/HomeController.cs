using com.jiechengbao.customerService.Models;
using com.jiechengbao.Ibll;
using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using POPO.Encrypt.Helper;
using Newtonsoft.Json.Linq;

namespace com.jiechengbao.customerService.Controllers
{
    public class HomeController : Controller
    {
        private IMemberBLL _memberBLL;
        public HomeController(IMemberBLL memberBLL)
        {
            _memberBLL = memberBLL;
        }

        /// <summary>
        /// 获取客户列表  返回的json
        /// 供 mui app 方便使用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetContacts()
        {
            List<CustomerModel> cmList = new List<Models.CustomerModel>();

            try
            {
                foreach (var item in _memberBLL.GetAllNoDeletedMembers().OrderByDescending(n=>n.CreatedTime).ToList())
                {
                    CustomerModel cm = new CustomerModel();
                    cm.HeadImage = item.HeadImage;
                    cm.NickName = item.NickeName;
                    cm.OpenId = item.OpenId;

                    cmList.Add(cm);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
            string result = string.Empty;
            try
            {
                DataContractJsonSerializer json = new DataContractJsonSerializer(cmList.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    json.WriteObject(stream, cmList);
                    result = Encoding.UTF8.GetString(stream.ToArray());
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
            }
            return Json(result);
        }

        /// <summary>
        /// 获取客服随机码  其实就是 guid
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetRandomCode()
        {
            string id = string.Empty;
            bool code = false;
            try
            {
                id = Guid.NewGuid().ToString().Replace("-", "");
                code = true;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                code = false;
            }
            var obj = new
            {
                code = code,
                id = id
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 检测新的用户 返回空值 或者是 json 字符串
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CheckNewMembers()
        {
            List<Member> memberList = _memberBLL.GetNewMembers5Min().ToList();
            List<CustomerModel> cmList = new List<Models.CustomerModel>();

            try
            {
                foreach (var item in memberList)
                {
                    CustomerModel cm = new Models.CustomerModel();
                    cm.HeadImage = item.HeadImage;
                    cm.NickName = item.NickeName;
                    cm.OpenId = item.OpenId;

                    cmList.Add(cm);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
            }

            string result = string.Empty;

            try
            {
                DataContractJsonSerializer json = new DataContractJsonSerializer(cmList.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    json.WriteObject(stream, cmList);
                    result = Encoding.UTF8.GetString(stream.ToArray());
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
            }
            return Json(result);
        }

        public ActionResult Chat(string EncryptOpenId)
        {
            if (!string.IsNullOrEmpty(EncryptOpenId))
            {
                string openId = EncryptHelper.Base64Decoding(EncryptOpenId);

                Member member = _memberBLL.GetMemberByOpenId(openId);

                if (member != null)
                {
                    ViewBag.OpenId = openId;
                    ViewBag.HeadImage = member.HeadImage;
                    ViewBag.NickName = member.NickeName;
                }
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult SetNickNames(string openId, string nickname)
        {
            var obj = new {
                res = false,
                nickname = ""
            };

            // 先判断 openId 是否为空
            if (string.IsNullOrEmpty(openId) || string.IsNullOrEmpty(nickname))
            {
                return Json(obj,JsonRequestBehavior.AllowGet);
            }

            bool res = false;

            string path = Server.MapPath("~/Json") + "\\data.json";
            List<CSModel> csList = new List<CSModel>();

            CSModel cs = null;
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    StreamReader sr = new StreamReader(fs);
                    // 读取 json 文件  获取 json 字符串
                    string jsonString = sr.ReadToEnd();

                    sr.Close();

                    JArray ja = (Newtonsoft.Json.Linq.JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);

                    csList = ja.ToObject<List<CSModel>>();
                    cs = csList.SingleOrDefault(n => n.OpenId == openId);
                }

                if (cs == null)
                {
                    CSModel cs_item = new CSModel()
                    {
                        OpenId = openId,
                        NickName = nickname
                    };

                    csList.Add(cs_item);
                }
                else
                {
                    cs.NickName = nickname;
                }

                using (FileStream fs = new FileStream (path,FileMode.OpenOrCreate,FileAccess.ReadWrite))
                {
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(csList);

                    StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);

                    sw.WriteLine(json);
                    sw.Close();

                    res = true;
                }
                
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);

                res = false;
            }
            string newName = string.Empty;

            if (res)
            {
                newName = nickname;
            }

            var newObj = new
            {
                res = res,
                nickname = newName
            };
            return Json(newObj, JsonRequestBehavior.AllowGet); 
        }

        [HttpPost]
        public ActionResult GetCSList()
        {
            string path = Server.MapPath("~/Json") + "\\data.json";
            string json = string.Empty;

            try
            {
                using (FileStream fs = new FileStream (path,FileMode.OpenOrCreate,FileAccess.Read))
                {
                    StreamReader sr = new StreamReader(fs);
                    json = sr.ReadToEnd();

                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
            }

            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}