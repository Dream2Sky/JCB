using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    /// <summary>
    /// 交易明细
    /// </summary>
    public class TransactionDetail:DataEntity
    {
        /// <summary>
        /// 谁的交易记录
        /// </summary>
        [Required]
        public Guid MemberId { get; set; }

        /// <summary>
        /// 交易备注
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string Notes { get; set; }

        /// <summary>
        /// 交易的金额
        /// </summary>
        [Required]
        public double Credit { get; set; }

        /// <summary>
        /// 积分的来源和去向 
        /// 
        /// 0代表微信支付购买商品  来源
        /// 1代表充值直接产生的积分  来源
        /// 2直接用积分购买商品  去向
        /// </summary>
        [Required]
        public int SourceOrDestination { get; set; }
    }
}
