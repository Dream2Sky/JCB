using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    /// <summary>
    /// 兑换服务类
    /// </summary>
    public class ExchangeService:DataEntity
    {
        /// <summary>
        /// 服务名
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        [MaxLength(255)]
        [Required]
        public string ImagePath { get; set; }

        /// <summary>
        /// 兑换所需积分
        /// </summary>
        [Required]
        public double Credit { get; set; }

        /// <summary>
        /// 服务的实际价格
        /// </summary>
        [Required]
        public double Price { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(255)]
        public string Notes { get; set; }

        /// <summary>
        /// 兑换服务的编号
        /// </summary>
        [Required]
        public string Code { get; set; }
    }
}
