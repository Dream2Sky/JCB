using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    public class Order:DataEntity
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string OrderNo { get; set; }

        /// <summary>
        /// 谁的订单
        /// </summary>
        [Required]
        public Guid MemberId { get; set; }

        /// <summary>
        /// 不要配送地址
        /// </summary>
        #region 坑啊  老板说不要配送地址 所以这个AddressId就没用了

        ///// <summary>
        ///// 配送的地址
        ///// </summary>
        //[Required]
        //public Guid AddressId { get; set; }

        #endregion


        public int PayWay { get; set; }

        public DateTime PayTime { get; set; }

        /// <summary>
        /// 订单的状态
        /// 0 未付款的订单
        /// 1 已付款的订单
        /// 2 已取消的订单
        /// </summary>
        [Required]
        public int Status { get; set; }

        [Required]
        public double TotalPrice { get; set; }
    }
}
