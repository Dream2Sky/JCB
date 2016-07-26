using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.jiechengbao.wx.Models
{
    public class MyAppointmentModel
    {
        public string carNo { get; set; }
        public string carInfo { get; set; }

        /// <summary>
        /// 预约时间
        /// </summary>
        public string appointmentTime { get; set; }
        /// <summary>
        /// 补充说明
        /// </summary>
        public string jcbremark { get; set; }

        /// <summary>
        /// 选择的预约服务列表
        /// </summary>
        public string[] appointmentItems { get; set; }
    }
}