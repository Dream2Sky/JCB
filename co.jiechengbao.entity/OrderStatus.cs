/*
    坑爹的老板说 不要配送功能 所以这个表示订单配送状态的表 就不用了
    
    但是不删
*/

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
