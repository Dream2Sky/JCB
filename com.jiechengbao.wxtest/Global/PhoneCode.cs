using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace com.jiechengbao.wx.Global
{
    public class PhoneCode
    {
        public bool GetCode(string phone, string msg)
        {
            bool result;
            string apiId = ConfigurationManager.AppSettings["MsgID"];
            string apiPwd = ConfigurationManager.AppSettings["MsgPwd"];
            string to = phone;

            Encoding myEncoding = Encoding.GetEncoding("GB2312");

            Random ro = new Random();
            string code = Convert.ToString(ro.Next(100000, 999999));
            System.Web.HttpContext.Current.Session["CertCode"] = code;
            string content = System.Web.HttpUtility.UrlEncode(string.Format("{0}{1}", msg, code), myEncoding);

            LogHelper.Log.Write("Content : " + content);

            string url = string.Format(ConfigurationManager.AppSettings["MsgUrl"]);
            if (SentMessage(url, apiId, apiPwd, to, content))
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;

        }

        public bool SentMessage(string url, string id, string pwd, string phone, string content)
        {
            string ret = string.Empty;
            try
            {
                url = string.Format(url + "?id={0}&pwd={1}&to={2}&content={3}", id, pwd, phone, content);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded;text/xml;charset=GB2312";


                WebResponse response = request.GetResponse();

                Stream stream = response.GetResponseStream();

                StreamReader reader = new StreamReader(stream);
                ret = reader.ReadToEnd();

                reader.Close();
                stream.Close();

                if (ret.Contains("000"))
                {
                    LogHelper.Log.Write("Contain 000");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                LogHelper.Log.Write("Exception");
                return false;
            }
        }
    }
}