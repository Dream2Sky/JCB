using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    /// <summary>
    /// 订单细节
    /// </summary>
    public class OrderDetail:DataEntity
    {
        /// <summary>
        /// 订单的 ID
        /// </summary>
        [Required]
        public Guid OrderId { get; set; }

        /// <summary>
        /// 订单的编号
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string OrderNo { get; set; }

        /// <summary>
        /// 谁的订单
        /// </summary>
        [Required]
        public Guid MemberId { get; set; }

        /// <summary>
        /// 配送的地址
        /// </summary>
        [Required]
        public Guid AddressId { get; set; }

        /// <summary>
        /// 绑定的商品Id
        /// </summary>
        [Required]
        public Guid GoodsId { get; set; }

        /// <summary>
        /// 提交订单时 商品的单价
        /// </summary>
        [Required]
        public double CurrentPrice { get; set; }

        /// <summary>
        /// 提交订单时 当时的折扣
        /// </summary>
        [Required]
        public double CurrentDiscount { get; set; }
    }
}
