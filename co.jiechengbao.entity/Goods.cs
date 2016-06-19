using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    public class Goods:DataEntity
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// 商品代号 编号
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string Code { get; set; }

        /// <summary>
        /// 商品的分类
        /// </summary>
        [Required]
        public Guid CategoryId { get; set; }

        /// <summary>
        /// 商品单价  当前单价
        /// </summary>
        [Required]
        public double Price { get; set; }

        /// <summary>
        /// 商品折扣
        /// </summary>
        [Required]
        public double Discount { get; set; }
    }
}
