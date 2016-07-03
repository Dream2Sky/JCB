using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    /// <summary>
    /// vip规则
    /// </summary>
    public class Rules:DataEntity
    {
        /// <summary>
        /// 会员累计的总积分
        /// </summary>
        [Required]
        public double TotalCredit { get; set; }

        /// <summary>
        /// 钱数对应的VIP等级
        /// </summary>
        [Required]
        public int VIP { get; set; }

        /// <summary>
        /// 根据VIP等级设定的商品折扣
        /// </summary>
        [Required]
        public double Discount { get; set; }
    }
}
