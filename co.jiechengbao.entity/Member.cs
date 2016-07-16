using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    /// <summary>
    /// 客户信息
    /// </summary>
    public class Member : DataEntity
    {
        /// <summary>
        /// 微信的openId
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string OpenId { get; set; }

        /// <summary>
        /// 微信的昵称
        /// </summary>
        [MaxLength(50)]
        public string NickeName { get; set; }

        /// <summary>
        /// 微信头像
        /// </summary>
        public string HeadImage { get; set; }
        /// <summary>
        /// 绑定的电话号码
        /// </summary>
        [MaxLength(20)]
        public string Phone { get; set; }

        #region 坑爹的老板 说要有个注册的功能 要用户填写姓名 所以要有个 realName字段
        /// <summary>
        /// 会员真实姓名
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string RealName { get; set; }

        #endregion

        /// <summary>
        /// vip等级 默认为0
        /// </summary>
        [Required]
        public int Vip { get; set; } = 0;

        #region 由于坑爹的老板改了需求 会员没有了余额的概念  然后要删掉这个字段

        ///// <summary>
        ///// 会员钱包余额
        ///// </summary>
        //[Required]
        //public double Assets { get; set; } = 0;
        #endregion

        /// <summary>
        /// 会员当前积分
        /// </summary>
        [Required]
        public double Credit { get; set; } = 0;

        /// <summary>
        /// 会员累计总积分
        /// </summary>
        [Required]
        public double TotalCredit { get; set; } = 0;
    }
}
