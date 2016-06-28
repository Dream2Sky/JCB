using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    /// <summary>
    /// 会员交易表
    /// </summary>
    public class Transaction : DataEntity
    {
        /// <summary>
        /// 交易的用户
        /// </summary>
        [Required]
        public Guid MemberId { get; set; }

        /// <summary>
        /// 交易的金额
        /// </summary>
        [Required]
        public double Amount { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        [Required]
        public int PayWay { get; set; }

        /// <summary>
        /// 为哪条订单支付
        /// </summary>
        [Required]
        public Guid OrderId { get; set; }
    }
}
