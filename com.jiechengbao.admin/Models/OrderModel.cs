using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.jiechengbao.admin.Models
{
    public class OrderModel:Order
    {
        public OrderModel()
        { }
        public OrderModel(Order order)
        {
            this.Id = order.Id;
            this.CreatedTime = order.CreatedTime;
            this.DeletedTime = order.DeletedTime;
            this.IsDeleted = order.IsDeleted;
            this.MemberId = order.MemberId;
            this.OrderNo = order.OrderNo;
            this.Status = order.Status;

            // 没有配送系统
            //this.AddressId = order.AddressId;
            this.PayTime = order.PayTime;
            this.PayWay = order.PayWay;
            this.TotalPrice = order.TotalPrice;
        }
        public string MemberName { get; set; }
        public string GoodsNameList { get; set; }

        // 配送系统没了  配送信息就没用了

        // public string Address { get; set; } //配送地址
        //public string Phone { get; set; } // 收货人电话号
        //public string Consignee { get; set; } // 收货人姓名
        //public int LogisticalStatus { get; set; } // 配送状态
    }
}