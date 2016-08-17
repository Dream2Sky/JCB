﻿using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using com.jiechengbao.wx.Global;
using com.jiechengbao.wx.Models;
using POPO.ActionFilter.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.wx.Controllers
{
    [WhitespaceFilter]
    [ETag]
    public class HomeController : Controller
    {
        private ICategoryBLL _categoryBLL;
        private IGoodsBLL _goodsBLL;
        private IGoodsImagesBLL _goodsImagesBLL;
        private IGoodsCategoryBLL _goodsCategoryBLL;
        private IRulesBLL _rulesBLL;
        private IMemberBLL _memberBLL;
        public HomeController(ICategoryBLL categoryBLL,IGoodsBLL goodsBLL, 
            IGoodsImagesBLL goodsImagesBLL, IGoodsCategoryBLL goodsCategoryBLL, IRulesBLL rulesBLL, IMemberBLL memberBLL)
        {
            _categoryBLL = categoryBLL;
            _goodsBLL = goodsBLL;
            _goodsImagesBLL = goodsImagesBLL;
            _goodsCategoryBLL = goodsCategoryBLL;
            _rulesBLL = rulesBLL;
            _memberBLL = memberBLL;
        }

        [IsLogin]
        public ActionResult Index()
        {    
            ViewData["CategoryList"] = _categoryBLL.GetAllCategory();
            return View();
        }

        [HttpPost]
        public PartialViewResult GoodsList(string category)
        {
            List<GoodsModel> gmList = new List<GoodsModel>();
            try
            {
                Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());
                double discount = _rulesBLL.GetDiscountByVIP(member.Vip);

                if (string.IsNullOrEmpty(category))
                {
                    foreach (var item in _goodsBLL.GetGoodsByCountOrderByCreatedTime(1))
                    {
                        if (item.IsDeleted == true)
                        {
                            continue;
                        }

                        GoodsModel gm = new GoodsModel(item);
                        GoodsImage gi = _goodsImagesBLL.GetPictureByGoodsId(item.Id);
                        if (gi == null)
                        {
                            continue;
                        }
                        gm.PicturePath = gi.ImagePath;
                        gm.Discount = discount;
                        gmList.Add(gm);
                    }
                }
                else
                {
                    Category currentCategory = _categoryBLL.GetCategoryByCategoryNo(category);
                    List<GoodsCategory> goodsCategoryList = _goodsCategoryBLL.GetGoodsCategoryListByCategoryId(currentCategory.Id).ToList();
                    foreach (var item in goodsCategoryList)
                    {
                        Goods goods = _goodsBLL.GetGoodsById(item.GoodsId);
                        // 判断商品是否是被删除的
                        if (goods.IsDeleted == true)
                        {
                            continue;
                        }
                        GoodsModel gm = new GoodsModel(goods);
                        GoodsImage gi = _goodsImagesBLL.GetPictureByGoodsId(goods.Id);

                        if (gi == null)
                        {
                            continue;
                        }
                        gm.PicturePath = gi.ImagePath;
                        gm.Discount = discount;
                        gmList.Add(gm);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
            }
            ViewData["GoodsModelList"] = gmList;
            return PartialView();
        }

        [HttpPost]
        public ActionResult GetGoodsList(string categoryCode)
        {
            // 先取出当前vip等级会员折扣
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());
            double discount = _rulesBLL.GetDiscountByVIP(member.Vip);

            List<GoodsModel> goodsList = new List<GoodsModel>();

            // 判断传递进来的categoryCode 是否为空

            // 如果为空 则搜索出所有没有标记为删除的Goods对象

            if (categoryCode == null)
            {
                // 得到所有Goods对象之后  遍历  构造出 GoodsModel list
                foreach (var item in _goodsBLL.GetAllNoDeteledGoods())
                {
                    GoodsModel gm = new GoodsModel(item);

                    GoodsImage gi = _goodsImagesBLL.GetPictureByGoodsId(gm.Id);

                    if (gi == null)
                    {
                        continue;
                    }

                    gm.PicturePath = _goodsImagesBLL.GetPictureByGoodsId(gm.Id).ImagePath;
                    gm.Discount = discount;
                    goodsList.Add(gm);
                }
            }
            else
            {
                // 如果传进来的categoryCode 不为空

                // 则要根据这个categoryCode 找到对应的Category对象

                // 然后通过找到的category对象 得到  GoodsCategoryList

                Category category = _categoryBLL.GetCategoryByCategoryNo(categoryCode);
                List<GoodsCategory> goodsCategoryList = _goodsCategoryBLL.GetGoodsCategoryListByCategoryId(category.Id).ToList();

                // 最后再构造 GoodModelList
                foreach (var item in goodsCategoryList)
                {
                    Goods good = _goodsBLL.GetGoodsById(item.GoodsId);

                    GoodsModel gm = new GoodsModel(_goodsBLL.GetGoodsById(item.GoodsId));

                    GoodsImage gi = _goodsImagesBLL.GetPictureByGoodsId(item.GoodsId);
                    if (gi == null)
                    {
                        continue;
                    }

                    gm.PicturePath = _goodsImagesBLL.GetPictureByGoodsId(item.GoodsId).ImagePath;
                    gm.Discount = discount;
                    goodsList.Add(gm);
                }
            }

            // 由于这个方法需要与前端传递 List对象

            // 所以要将等到的 GoodsModel List 序列化为 json字符串 传递到前端

            string jsonResult = string.Empty;
            try
            {
                jsonResult = ObjToJson<List<GoodsModel>>(goodsList);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
            return Content(jsonResult);
        }

        [NonAction]
        private string ObjToJson<T>(T data)
        {
            try
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(data.GetType());
                using (MemoryStream ms = new MemoryStream())
                {
                    serializer.WriteObject(ms, data);
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                return null;
            }
        }

        public ActionResult Detail(string code)
        {
            // 先取出当前vip等级会员折扣
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());
            double discount = _rulesBLL.GetDiscountByVIP(member.Vip);

            GoodsModel gm = new GoodsModel(_goodsBLL.GetGoodsByCode(code));
            if (gm == null)
            {
                return View(gm);
            }
            GoodsImage gi = _goodsImagesBLL.GetPictureByGoodsId(gm.Id);
            gm.PicturePath = gi.ImagePath;
            gm.Discount = discount;

            return View(gm);
        }
    }
}