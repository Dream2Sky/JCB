using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    /// <summary>
    /// 预约服务
    /// </summary>
    public class AppointmentService:DataEntity
    {
        /// <summary>
        /// 服务名
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 对应的编号
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Code { get; set; }
    }
}
