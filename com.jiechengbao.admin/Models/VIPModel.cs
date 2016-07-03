using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace com.jiechengbao.admin.Models
{
    [DataContract]
    public class VIPModel
    {
        [DataMember]
        public int VIP { get; set; }
        [DataMember]
        public double TotalCredit { get; set; }
        [DataMember]
        public double Discount { get; set; }
    }
}