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
                // 捷诚宝 AppId
                return "wx3fab45769c82a189";
            }
        }

        public static string AppSecret
        {
            get
            {
                // 捷诚宝 AppSecret
                return "1b4feb651f6bbae81776068c241f8603";
            }
        }

        public static string WxDomain
        {
            get
            {
                return "jcb.ybtx88.com";
            }
        }
    }
}