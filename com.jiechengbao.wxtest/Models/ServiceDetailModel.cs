using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.jiechengbao.wx.Models
{
    public class ServiceDetailModel
    {
        /// <summary>
        /// 服务Id
        /// </summary>
        public Guid ServcieId { get; set; }
        /// <summary>
        /// 服务图片路径
        /// </summary>
        public string ServiceImagePath { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 当前服务剩余使用次数
        /// </summary>
        public int CurrentCount { get; set; }

        /// <summary>
        /// 当前服务的总使用次数
        /// </summary>
        public int TotalCount { get; set; }
    }
}