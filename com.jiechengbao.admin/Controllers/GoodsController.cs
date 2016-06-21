using com.jiechengbao.admin.Models;
using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using com.jiechengbao.common;

namespace com.jiechengbao.admin.Controllers
{
    public class GoodsController : Controller
    {
        private IGoodsBLL _goodsBLL;
        private ICategoryBLL _categoryBLL;
        private IGoodsCategoryBLL _goodsCategoryBLL;
        private IGoodsImagesBLL _goodsImagesBLL;

        public GoodsController(IGoodsBLL goodsBLL, ICategoryBLL categoryBLL, IGoodsCategoryBLL goodsCategoryBLL, IGoodsImagesBLL goodsImagesBLL)
        {
            _goodsBLL = goodsBLL;
            _categoryBLL = categoryBLL;
            _goodsCategoryBLL = goodsCategoryBLL;
            _goodsImagesBLL = goodsImagesBLL;
        }

        public ActionResult List()
        {
            ViewData["GoodsList"] = _goodsBLL.GetAllNoDeteledGoods();
            return View();
        }

        public ActionResult Add(string msg)
        {
            ViewBag.Msg = msg;
            ViewData["CategoryList"] = _categoryBLL.GetAllCategory();
            return View();
        }

        [HttpPost]
        public ActionResult Add(GoodsModel model)
        {
            // 判断传递的值是否为空 
            if (model == null)
            {
                // 是空 则直接返回
                return RedirectToAction("Add", new { msg = "提交的数据为空，请重新提交" });
            }

            // 构造 Goods 对象
            #region 构造 Goods 对象   

            Goods goods = new Goods();
            goods.Id = Guid.NewGuid();
            goods.IsDeleted = false;
            goods.Name = model.Name;
            goods.Price = model.Price;
            goods.Code = "Goods_" + TimeManager.GetCurrentTimestamp();
            goods.CreatedTime = DateTime.Now.Date;
            goods.Description = model.Description;
            goods.Discount = model.Discount;

            #endregion

            // 添加新的Goods 对象

            if (_goodsBLL.Add(goods))
            {
                #region 此处应该用事务来做 但是现在简易版就随意一点 将来这里必须用事务

                #endregion

                // 添加成功后 构造 goodsimage 对象
                // 并添加到数据库
                GoodsImage gi = new GoodsImage();
                gi.Id = Guid.NewGuid();
                gi.ImagePath = model.PicturePath;
                gi.CreatedTime = DateTime.Now.Date;
                gi.GoodsId = goods.Id;
                gi.IsDeleted = false;

                _goodsImagesBLL.Add(gi);

                // 添加成功 则 遍历传递过来的分类列表
                // 并添加到数据库
                foreach (var item in model.CategoryList)
                {
                    // 构造 GoodsCategory 对象
                    GoodsCategory gc = new GoodsCategory();
                    gc.Id = Guid.NewGuid();
                    gc.CreatedTime = DateTime.Now.Date;
                    gc.CategoryId = _categoryBLL.GetCategoryByCategoryNo(item).Id;
                    gc.IsDeleted = false;
                    gc.GoodsId = goods.Id;

                    _goodsCategoryBLL.Add(gc);
                }

                return RedirectToAction("Add", new { msg = "添加成功" });
            }
            else
            {
                return RedirectToAction("Add", new { msg = "添加失败" });
            }
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            try
            {
                string fileName = EncryptManager.SHA1(file.FileName + TimeManager.GetCurrentTimestamp()) + ".jpg";
                string path = System.IO.Path.Combine(Server.MapPath("~/Uploads"), System.IO.Path.GetFileName(fileName));
                file.SaveAs(path);

                return Json(fileName, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                return Json("False", JsonRequestBehavior.AllowGet);
            }
        }
    }
}