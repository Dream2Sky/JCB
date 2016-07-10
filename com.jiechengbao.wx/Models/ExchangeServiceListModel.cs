using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.jiechengbao.wx.Models
{
    public class ExchangeServiceListModel
    {
        /// <summary>
        /// 兑换服务Id
        /// </summary>
        public Guid ExchangeServiceRecordId { get; set; }

        /// <summary>
        ///  兑换服务名
        /// </summary>
        public string ExchangeServiceName { get; set; }

        /// <summary>
        /// 商品图片
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// 购买时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
    }
}