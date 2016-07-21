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
        /// 预约了什么服务
        /// </summary>
        [Required]
        public Guid AppointmentServiceId { get; set; }

        /// <summary>
        /// 预约的时间段
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string TimePeriod { get; set; }
    }
}
