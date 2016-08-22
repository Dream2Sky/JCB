using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.jiechengbao.admin.Models
{
    public class AdModel
    {
        public string AdCode { get; set; }
        public string AdName { get; set; }
        public string AdDescription { get; set; }
        public string AdImagePath { get; set; }
        public bool IsRecommend { get; set; }
        public string Category { get; set; }
        public string CategoryCode { get; set; }
    }
}