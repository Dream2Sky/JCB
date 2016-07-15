using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    public class OrderStatus:DataEntity
    {
        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public int Status { get; set; }
    }
}
