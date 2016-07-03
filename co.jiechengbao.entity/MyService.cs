using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    /// <summary>
    /// 我的服务  记录用户购买的服务
    /// </summary>
    public class MyService:DataEntity
    {
        /// <summary>
        /// 谁的服务
        /// </summary>
        [Required]
        public Guid MemberId { get; set; }

        /// <summary>
        /// 服务的Id
        /// </summary>
        [Required]
        public Guid GoodsId { get; set; }

        /// <summary>
        /// 服务的名称
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string GoodsName { get; set; }

        /// <summary>
        /// 当前服务剩余次数
        /// </summary>
        [Required]
        public int CurrentCount { get; set; }

        /// <summary>
        /// 服务的总次数
        /// </summary>
        [Required]
        public int TotalCount { get; set; }
    }
}
