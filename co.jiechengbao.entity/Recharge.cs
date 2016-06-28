﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    /// <summary>
    /// 会员充值表
    /// </summary>
    public class Recharge:DataEntity
    {
        /// <summary>
        /// 充值会员
        /// </summary>
        [Required]
        public Guid MemberId { get; set; }

        /// <summary>
        /// 充值金额
        /// </summary>
        [Required]
        public double Amount { get; set; }

        /// <summary>
        /// 充值方式
        /// </summary>
        [Required]
        public string Payway { get; set; }
    }
}
