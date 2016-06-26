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
            this.AddressId = order.AddressId;
            this.PayTime = order.PayTime;
            this.PayWay = order.PayWay;
        }
        public string MemberName { get; set; }
    }
}