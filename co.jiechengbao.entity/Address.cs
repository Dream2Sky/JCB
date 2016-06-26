using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    /// <summary>
    /// 配送地址
    /// </summary>
    public class Address:DataEntity
    {
        /// <summary>
        /// 谁家的地址
        /// </summary>
        [Required]
        public Guid MemberId{ get; set; }

        [Required]
        [MaxLength(30)]
        public string Province { get; set; }

        [Required]
        [MaxLength(30)]
        public string City { get; set; }
        [Required]
        [MaxLength(30)]
        public string County { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Detail { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string Consignee { get; set; }
       
        /// <summary>
        /// 收货人 电话
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }

        /// <summary>
        /// 是否是默认地址
        /// </summary>
        [Required]
        public bool IsDefault { get; set; }

    }
}
