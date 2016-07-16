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
    public class Goods : DataEntity
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        [Required]
        [MaxLength(255)]
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 商品代号 编号
        /// </summary>
        [Required]
        [MaxLength(30)]
        [DataMember]
        public string Code { get; set; }

        /// <summary>
        /// 商品单价  当前单价 == vip0价格
        /// </summary>
        [Required]
        [DataMember]
        public double Price { get; set; }

        ///// <summary>
        ///// 商品折扣
        ///// </summary>
        //[Required]
        //[DataMember]
        //public double Discount { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        [MaxLength(255)]
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// 当该商品为服务类型时 
        /// 该字段表示服务的次数
        /// 次数由管理员添加商品的时候设置
        /// </summary>
        [Required]
        [DataMember]
        public int ServiceCount { get; set; }

        /// <summary>
        /// 商品原价
        /// </summary>
        [Required]
        [DataMember]
        public double OriginalPrice { get; set; }

        /// <summary>
        /// 商品的兑换积分
        /// </summary>
        [Required]
        [DataMember]
        public double ExchangeCredit { get; set; }

    }
}
