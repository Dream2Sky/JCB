using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    /// <summary>
    /// 商品图片路径集合
    /// </summary>
    public class GoodsImage:DataEntity
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        [Required]
        public Guid GoodsId { get; set; }

        /// <summary>
        /// 商品图片路径
        /// </summary>
        [MaxLength(255)]
        [Required]
        public string ImagePath { get; set; }
    }
}
