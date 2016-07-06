using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.jiechengbao.wx.Models
{
    public class Access_Token_JsonModel
    {
        /// <summary>
        /// 网页授权token
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// access_token授权过期时间 默认是7200s
        /// </summary>
        public string expires_in { get; set; }

        /// <summary>
        /// 刷新token
        /// </summary>
        public string refresh_token { get; set; }

        /// <summary>
        /// 微信 openid
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 微信授权作用域
        /// </summary>
        public string scope { get; set; }
    }
}