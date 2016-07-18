using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    public class CreditRecord:DataEntity
    {
        /// <summary>
        /// 谁的操作
        /// </summary>
        [Required]
        public Guid MemberId { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string OperationType { get; set; } 
        /// <summary>
        /// 涉及的金额
        /// </summary>
        [Required]
        public double Money { get; set; }
        /// <summary>
        /// 当前积分系数
        /// </summary>
        public double CurrentCreditCoefficient { get; set; }

        /// <summary>
        /// 备注 也是标题
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string Notes { get; set; }
    }
}
