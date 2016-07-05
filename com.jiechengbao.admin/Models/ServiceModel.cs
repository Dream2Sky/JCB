using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.jiechengbao.admin.Models
{
    public class MyServiceModel
    {
        public string MemberName { get; set; }
        public string MemberImage { get; set; }
        public string ServiceName { get; set; }
        public int CurrentCount { get; set; }
        public int TotalCount { get; set; }
        public DateTime CreateTime { get; set; }
    }
}