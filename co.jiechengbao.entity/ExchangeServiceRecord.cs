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
        public bool IsUse { get; set; }
    }
}
