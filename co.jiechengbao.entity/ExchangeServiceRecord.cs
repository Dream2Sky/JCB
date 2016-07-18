/*
 没有积分商城 
 
 就没有了积分兑换的兑换的记录

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    /// <summary>
    /// 兑换服务的交易记录仪=
    /// </summary>
    public class ExchangeServiceRecord:DataEntity
    {
        [Required]
        public Guid MemberId { get; set; }

        [Required]
        public Guid ExchangeSerivceId { get; set; }

        [Required]
        public double Credit { get; set; }

        [Required]
        [MaxLength(255)]
        public string QRPath { get; set; }

        [Required]
        public bool IsUse { get; set; }
    }
}
