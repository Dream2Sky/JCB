using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    /// <summary>
    /// 我的预约服务
    /// </summary>
    public class MyAppointment:DataEntity
    {
        /// <summary>
        /// 谁的预约
        /// </summary>
        [Required]
        public Guid MemberId { get; set; }

        /// <summary>
        /// 预约的时间段
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string AppointmentTime { get; set; }

        /// <summary>
        /// 该预约订单的价钱
        /// </summary>
        [Required]
        public double Price { get; set; }

        /// <summary>
        /// 该预约订单是否付款
        /// </summary>
        [Required]
        public bool IsPay { get; set; }

        /// <summary>
        /// 补充说明
        /// </summary>
        [MaxLength(255)]
        public string Supplement { get; set; }

        /// <summary>
        /// 备注  我们的员工 再修改预约单时 所选填的备注
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [Required]
        [MaxLength(15)]
        public string CarNumber { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string CarInfo { get; set; }

    }
}
