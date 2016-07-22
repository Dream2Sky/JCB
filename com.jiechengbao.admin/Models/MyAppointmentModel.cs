using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.jiechengbao.admin.Models
{
    public class MyAppointmentModel
    {
        public string NickName { get; set; }
        public string HeadImage { get; set; }
        public Guid MyAppointmentId { get; set; }
        /// <summary>
        /// 会员所预约的服务项
        /// </summary>
        public List<string> AppointmentNameList { get; set; }
        public string AppointmentTime { get; set; }
        public bool IsPay { get; set; }
        public double Price { get; set; }
        public string Notes { get; set; }
        /// <summary>
        /// 问题描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 补充说明
        /// </summary>
        public string Supplement { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNumber { get; set; }
        /// <summary>
        /// 车型
        /// </summary>
        public string CarInfo { get; set; }

    }
}