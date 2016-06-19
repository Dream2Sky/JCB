using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    /// <summary>
    /// 购物车
    /// </summary>
    public class Cart:DataEntity
    {
        /// <summary>
        /// 谁的购物车
        /// </summary>
        [Required]
        public Guid MemberId { get; set; }

        /// <summary>
        /// 购物车里放了啥
        /// </summary>
        [Required]
        public Guid GoodsId { get; set; }

        /// <summary>
        /// 对应商品的数量
        /// </summary>
        [Required]
        public int Count { get; set; }
    }
}
