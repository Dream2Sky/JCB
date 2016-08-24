using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.common
{
    public class HttpManager
    {
        public static string AccessURL_GET(string url)
        {
            string result = string.Empty;
            try
            {
                HttpWebRequest r = (HttpWebRequest)WebRequest.Create(url);
                r.Method = "Get";

                HttpWebResponse res = (HttpWebResponse)r.GetResponse();

                Stream sr = res.GetResponseStream();
                StreamReader sre = new StreamReader(sr);


                result = sre.ReadToEnd();

            }
            catch (Exception)
            {
                return null;
            }

            return result;
        }

        public static string AccessURL_GET(string url,CookieContainer cookies)
        {
            string result = string.Empty;
            try
            {
                HttpWebRequest r = (HttpWebRequest)WebRequest.Create(url);
                
                r.Method = "Get";
                r.CookieContainer = cookies;
                r.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:47.0) Gecko/20100101 Firefox/47.0";
                HttpWebResponse res = (HttpWebResponse)r.GetResponse();

                Stream sr = res.GetResponseStream();
                StreamReader sre = new StreamReader(sr);


                result = sre.ReadToEnd();

            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                return null;
            }

            return result;
        }

        public static string AccessURL_POST(string url, string postData)
        {
            string result = string.Empty;
            try
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(byteArray, 0, byteArray.Length);

                requestStream.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);

                result = reader.ReadToEnd();
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
    }
}
