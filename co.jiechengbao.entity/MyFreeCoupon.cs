using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    public class MyFreeCoupon:DataEntity
    {
        [Required]
        public Guid FreeCouponId { get; set; }

        [Required]
        public Guid MemberId{ get; set; }

        /// <summary>
        /// 优惠券消费二维码 路径
        /// </summary>
        [MaxLength(255)]
        public string FreeCouponQRs { get; set; }
    }
}
