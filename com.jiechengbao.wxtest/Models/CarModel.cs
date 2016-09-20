using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.jiechengbao.wx.Models
{
    public class CarModel
    {
        /// <summary>
        /// 车牌号
        /// </summary>
        public string Numberplate { get; set; }

        /// <summary>
        /// 车架号
        /// </summary>
        public string ChassisNumber { get; set; }

        /// <summary>
        /// 发动机号
        /// </summary>
        public string EngineNumber { get; set; }

        /// <summary>
        /// 汽车其他信息
        /// </summary>
        public string CarDetailInfo { get; set; }
    }
}