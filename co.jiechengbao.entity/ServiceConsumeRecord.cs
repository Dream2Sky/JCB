using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    /// <summary>
    /// 服务的消费记录表
    /// </summary>
    public class ServiceConsumeRecord:DataEntity
    {
        [Required]
        public Guid ServiceId { get; set; }
    }
}
