using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.jiechengbao.admin.Models
{
    public class ExchangeServiceModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public double Credit { get; set; }
        public double Price { get; set; }
        public string Notes { get; set; }
    }
}