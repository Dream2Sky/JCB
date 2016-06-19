using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    /// <summary>
    /// 客户信息
    /// </summary>
    public class Member:DataEntity
    {
        /// <summary>
        /// 微信的openId
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string OpenId { get; set; }

        /// <summary>
        /// 微信的昵称
        /// </summary>
        [MaxLength(50)]
        public string NickeName { get; set; }

        /// <summary>
        /// 微信头像
        /// </summary>
        public string HeadImage { get; set; }
        /// <summary>
        /// 绑定的电话号码
        /// </summary>
        [MaxLength(20)]
        public string Phone { get; set; }
    }
}
