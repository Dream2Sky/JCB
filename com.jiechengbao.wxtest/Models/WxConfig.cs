using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.jiechengbao.wx.Models
{
    public class WxConfig
    {
        public static string AppId
        {
            get
            {
                // 东方宝源测试公众号 AppId
                return "wx084f8aeacccb083b";
            }
        }

        public static string AppSecret
        {
            get
            {
                // 东方宝源测试公众号 AppSecret
                return "037fca3cbdcc6e9fe23dba081c25ebf6";
            }
        }

        public static string WxDomain
        {
            get
            {
                return "jcbtest.ybtx88.com";
            }
        }
    }

}