using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    /// <summary>
    /// 记录兑换商品的消费二维码
    /// </summary>
    public class ExchangeServiceQR:DataEntity
    {
        /// <summary>
        /// 谁的二维码
        /// </summary>
        [Required]
        public Guid MemberId { get; set; }

        /// <summary>
        /// 哪个商品Id
        /// </summary>
        [Required]
        public Guid ExchangeServiceId { get; set; }

        /// <summary>
        /// 二维码的路径
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string QRPath { get; set; }
    }
}
