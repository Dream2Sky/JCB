using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.jiechengbao.wx.Models
{
    public class UserInfo_JsonModel
    {
        /// <summary>
        /// 用户微信openID
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 微信用户昵称
        /// </summary>
        public string nickname { get; set; }
        public int sex { get; set; }

        /// <summary>
        /// 微信头像url
        /// </summary>
        public string headimgurl { get; set; }
    }
}