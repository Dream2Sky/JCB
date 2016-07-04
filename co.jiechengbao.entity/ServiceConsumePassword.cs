using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    /// <summary>
    /// 服务消费密码
    /// </summary>
    public class ServiceConsumePassword:DataEntity
    {
        /// <summary>
        /// 消费密码
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string Password { get; set; }
    }
}
