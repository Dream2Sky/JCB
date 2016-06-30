using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    /// <summary>
    /// 汽车信息
    /// </summary>
    public class Car:DataEntity
    {
        /// <summary>
        /// 谁的车
        /// </summary>
        [Required]
        public Guid MemberId { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Numberplate { get; set; }

        /// <summary>
        /// 车架号
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string ChassisNumber { get; set; }

        /// <summary>
        /// 发动机号
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string EngineNumber { get; set; }

        /// <summary>
        /// 汽车其他信息
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string CarDetailInfo { get; set; }
    }
}
