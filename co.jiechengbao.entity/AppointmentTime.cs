using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    /// <summary>
    /// 预约时间段
    /// </summary>
    public class AppointmentTime:DataEntity
    {
        /// <summary>
        /// 时间段  设为字符串类型 
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string TimePeriod { get; set; }
    }
}
