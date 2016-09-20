using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.jiechengbao.wx.Models
{
    public class AddressModel
    {
        public string Province { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Detail { get; set; }
        public string Consignee { get; set; }
        public string Phone { get; set; }

    }
}