using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.jiechengbao.wx.Models
{
    public class AdModel
    {
        public string AdName { get; set; }
        public string AdCode { get; set; }
        public string AdDescription { get; set; }
        public string AdImagePath { get; set; }
        public bool IsRecommand { get; set; }
    }
}