using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    /// <summary>
    /// 记录服务的消费二维码
    /// </summary>
    public class ServiceQR:DataEntity
    {
        /// <summary>
        /// 谁的二维码
        /// </summary>
        [Required]
        public Guid MemberId { get; set; }

        /// <summary>
        /// 哪个服务
        /// </summary>
        [Required]
        public Guid ServcieId { get; set; }

        /// <summary>
        /// 二维码的路径
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string QRPath { get; set; }
    }
}
