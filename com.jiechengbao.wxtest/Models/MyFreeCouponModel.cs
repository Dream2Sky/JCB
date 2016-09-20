using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.jiechengbao.wx.Models
{
    public class MyFreeCouponModel
    {
        public Guid myFreeCouponId { get; set; }
        public string FreeCouponName { get; set; }
        public double Price { get; set; }
        public string Descritption { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}