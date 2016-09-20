using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.jiechengbao.wx.Models
{
    public class ServiceModel
    {
        /// <summary>
        /// 会员昵称
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        /// 当前操作的服务名
        /// </summary>
        public string ServiceName { get; set; }    

        /// <summary>
        /// 当前操作的服务Id
        /// </summary>
        public Guid ServiceId { get; set; }
         
    }
}