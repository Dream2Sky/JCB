using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.jiechengbao.admin.Models
{
    public class GoodsModel:Goods
    {
        public GoodsModel()
        { }
        public GoodsModel(Goods goods)
        {
            this.Code = goods.Code;
            this.CreatedTime = goods.CreatedTime;
            this.DeletedTime = goods.DeletedTime;
            this.Id = goods.Id;
            this.IsDeleted = goods.IsDeleted;
            this.Name = goods.Name;
            this.Price = goods.Price;
            this.Description = goods.Description;
            this.ServiceCount = goods.ServiceCount;
            this.OriginalPrice = goods.OriginalPrice;
            this.ExchangeCredit = goods.ExchangeCredit;
        }
        public bool IsRecommend { get; set; } // 是否是推荐商品
        public string PicturePath { get; set; }
        public string[] CategoryList { get; set; }
    }
}