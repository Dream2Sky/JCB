using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace com.jiechengbao.customerService.Models
{
    /// <summary>
    /// 客户列表的数据结构
    /// </summary>
    [DataContract]
    public class CustomerModel
    {
        /// <summary>
        /// 客户昵称
        /// </summary>
        [DataMember]
        public string NickName { get; set; }

        /// <summary>
        /// 客户头像
        /// </summary>
        [DataMember]
        public string HeadImage { get; set; }

        /// <summary>
        /// 客户微信openId
        /// 也是 客户会话Id  用作会话标记
        /// </summary>
        [DataMember]
        public string OpenId { get; set; }
    }
}