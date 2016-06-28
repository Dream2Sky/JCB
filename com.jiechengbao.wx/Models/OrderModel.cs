using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.jiechengbao.wx.Models
{
    public class OrderModel:Order
    {
        public OrderModel()
        { }
        public OrderModel(Order order)
        {
            this.Id = order.Id;
            this.AddressId = order.AddressId;
            this.CreatedTime = order.CreatedTime;
            this.DeletedTime = order.DeletedTime;
            this.IsDeleted = order.IsDeleted;
            this.MemberId = order.MemberId;
            this.OrderNo = order.OrderNo;
            this.PayTime = order.PayTime;
            this.PayWay = order.PayWay;
            this.Status = order.Status;
            this.TotalPrice = order.TotalPrice;
        }

        public List<OrderDetailModel> GoodsModelList { get; set; }
    }
}