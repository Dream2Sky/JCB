using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.jiechengbao.wx.Models
{
    public class TransactionModel
    {
        public double Amount { get; set; }
        public string OrderNo { get; set; }
        public DateTime CreateTime { get; set; }
    }
}