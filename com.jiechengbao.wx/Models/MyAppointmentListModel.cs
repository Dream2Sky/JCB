using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.jiechengbao.wx.Models
{
    public class MyAppointmentListModel
    {
        public Guid MyAppointmentId { get; set; }
        public string CarNumber { get; set; }
        public string AppointmentTime { get; set; }
        public bool IsPay { get; set; }
        public double Price { get; set; }
        public string AppointmentServiceItems { get; set; }
    }
}