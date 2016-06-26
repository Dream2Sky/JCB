using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.jiechengbao.wx.Models
{
    public class OrderDetailModel:OrderDetail
    {
        public OrderDetailModel() { }
        public OrderDetailModel(OrderDetail order)
        {
            this.Count = order.Count;
            this.CreatedTime = order.CreatedTime;
            this.CurrentDiscount = order.CurrentDiscount;
            this.CurrentPrice = order.CurrentPrice;
            this.DeletedTime = order.DeletedTime;
            this.GoodsId = order.GoodsId;
            this.Id = order.Id;
            this.IsDeleted = order.IsDeleted;
            this.OrderId = order.OrderId;
            this.OrderNo = order.OrderNo;
            this.TotalPrice = order.TotalPrice;
        }

        public string GoodsName { get; set; }
        public string PicturePath { get; set; }
    }
}