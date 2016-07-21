using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    public class MyAppointmentItem:DataEntity
    {
        /// <summary>
        /// 我的预约单的Id
        /// </summary>
        [Required]
        public Guid MyAppointmentId { get; set; }

        /// <summary>
        /// 预约了什么服务
        /// </summary>
        [Required]
        public Guid AppointmentServiceId { get; set; }
    }
}
