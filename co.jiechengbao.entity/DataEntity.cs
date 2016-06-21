using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace com.jiechengbao.entity
{
    [DataContract]
    public class DataEntity
    {
        /// <summary>
        /// key
        /// </summary>
        [Key]
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [DataMember]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 判断是否被删除了 标记已删除的记录
        /// </summary>
        [Required]
        [DataMember]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        [DataMember]
        public DateTime DeletedTime { get; set; }
    }
}
