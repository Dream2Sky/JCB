using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using com.jiechengbao.wx.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.wx.Controllers
{
    public class HomeController : Controller
    {
        private ICategoryBLL _categoryBLL;
        private IGoodsBLL _goodsBLL;
        private IGoodsImagesBLL _goodsImagesBLL;
        private IGoodsCategoryBLL _goodsCategoryBLL;
        public HomeController(ICategoryBLL categoryBLL,IGoodsBLL goodsBLL, IGoodsImagesBLL goodsImagesBLL, IGoodsCategoryBLL goodsCategoryBLL)
        {
            _categoryBLL = categoryBLL;
            _goodsBLL = goodsBLL;
            _goodsImagesBLL = goodsImagesBLL;
            _goodsCategoryBLL = goodsCategoryBLL;
        }

        public ActionResult Index()
        {
            ViewData["CategoryList"] = _categoryBLL.GetAllCategory();
            return View();
        }

        [HttpPost]
        public ActionResult GetGoodsList(string categoryCode)
        {
            List<GoodsModel> goodsList = new List<GoodsModel>();

            // 判断传递进来的categoryCode 是否为空

            // 如果为空 则搜索出所有没有标记为删除的Goods对象

            if (categoryCode == null)
            {
                // 得到所有Goods对象之后  遍历  构造出 GoodsModel list
                foreach (var item in _goodsBLL.GetAllNoDeteledGoods())
                {
                    GoodsModel gm = new GoodsModel(item);
                    gm.PicturePath = _goodsImagesBLL.GetPictureByGoodsId(gm.Id).ImagePath;

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
                    GoodsModel gm = new GoodsModel(_goodsBLL.GetGoodsById(item.GoodsId));
                    gm.PicturePath = _goodsImagesBLL.GetPictureByGoodsId(item.GoodsId).ImagePath;

                    goodsList.Add(gm);
                }
            }

            // 由于这个方法需要与前端传递 List对象

            // 所以要将等到的 GoodsModel List 序列化为 json字符串 传递到前端

            string jsonResult = string.Empty;
            try
            {
                // 序列化
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


        public ActionResult Detail(string goodsCode)
        {
            return View();
        }
    }
}