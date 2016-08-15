using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    public class FreeCoupon:DataEntity
    {
        [Required]
        [MaxLength(35)]
        public string CouponCode { get; set; }
        [Required]
        [MaxLength(50)]
        public string  CouponName { get; set; }

        [Required]
        public double Price { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
    }
}
