using com.jiechengbao.admin.Models;
using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using com.jiechengbao.common;
using System.Threading;

namespace com.jiechengbao.admin.Controllers
{
    public class GoodsController : Controller
    {
        private delegate void DelGoodsCategoryDel(List<GoodsCategory> gcList);

        private IGoodsBLL _goodsBLL;
        private ICategoryBLL _categoryBLL;
        private IGoodsCategoryBLL _goodsCategoryBLL;
        private IGoodsImagesBLL _goodsImagesBLL;
        private IReCommendBLL _recommendBLL;
        public GoodsController(IGoodsBLL goodsBLL, ICategoryBLL categoryBLL,
            IGoodsCategoryBLL goodsCategoryBLL, IGoodsImagesBLL goodsImagesBLL,
            IReCommendBLL recommendBLL)
        {
            _goodsBLL = goodsBLL;
            _categoryBLL = categoryBLL;
            _goodsCategoryBLL = goodsCategoryBLL;
            _goodsImagesBLL = goodsImagesBLL;
            _recommendBLL = recommendBLL;
        }

        public ActionResult List()
        {
            List<GoodsModel> modelList = new List<GoodsModel>();
            foreach (var item in _goodsBLL.GetAllNoDeteledGoods())
            {
                GoodsModel gm = new GoodsModel(item);
                gm.PicturePath = _goodsImagesBLL.GetPictureByGoodsId(item.Id).ImagePath;
                gm.IsRecommend = _recommendBLL.IsRecommend(gm.Id);
                modelList.Add(gm);
            }
            ViewData["GoodsList"] = modelList;
            return View();
        }

        /// <summary>
        /// 商品搜索
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult List(string condition)
        {
            List<GoodsModel> modelList = new List<GoodsModel>();
            foreach (var item in _goodsBLL.GetGoodsByCondition(condition))
            {
                GoodsModel gm = new GoodsModel(item);
                gm.PicturePath = _goodsImagesBLL.GetPictureByGoodsId(item.Id).ImagePath;
                gm.IsRecommend = _recommendBLL.IsRecommend(gm.Id);

                modelList.Add(gm);
            }

            ViewData["GoodsList"] = modelList;
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
            goods.ServiceCount = model.ServiceCount;
            goods.DeletedTime = DateTime.MinValue.AddHours(8);

            // 商品新家字段  OriginalPrice  原价  

            // 该字段不影响原来商品添加的逻辑
            goods.OriginalPrice = model.OriginalPrice;


            #endregion

            // 添加新的Goods 对象

            if (_goodsBLL.Add(goods))
            {
                #region 此处应该用事务来做 但是现在简易版就随意一点 将来这里必须用事务

                #endregion

                // 添加成功后 构造 goodsimage 对象
                // 并添加到数据库

                LogHelper.Log.Write("添加商品成功,现在正在构造goodsImage");

                GoodsImage gi = new GoodsImage();
                gi.Id = Guid.NewGuid();
                gi.ImagePath = model.PicturePath;
                gi.CreatedTime = DateTime.Now.Date;
                gi.GoodsId = goods.Id;
                gi.IsDeleted = false;
                gi.DeletedTime = DateTime.MinValue.AddHours(8);

                _goodsImagesBLL.Add(gi);

                LogHelper.Log.Write("添加商品图片成功");

                // 添加成功 则 遍历传递过来的分类列表
                // 并添加到数据库
                foreach (var item in model.CategoryList)
                {
                    if (item == null)
                    {
                        LogHelper.Log.Write("获取商品分类列表失败");
                    }
                    // 构造 GoodsCategory 对象
                    GoodsCategory gc = new GoodsCategory();
                    gc.Id = Guid.NewGuid();
                    gc.CreatedTime = DateTime.Now.Date;
                    gc.CategoryId = _categoryBLL.GetCategoryByCategoryNo(item).Id;
                    gc.IsDeleted = false;
                    gc.GoodsId = goods.Id;
                    gc.DeletedTime = DateTime.MinValue.AddHours(8);

                    _goodsCategoryBLL.Add(gc);
                }

                Thread.Sleep(2000);

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

        /// <summary>
        /// </summary>
        /// <param name="GoodsCode"></param>
        /// <returns></returns>
        public ActionResult Update(string GoodsCode)
        {
            // 根据 传递过来的goodsCode 获取对应的goods 
            Goods goods = _goodsBLL.GetGoodsByCode(GoodsCode);

            // 再生成 goodsModel 这个将作为model传递到前台
            GoodsModel gm = new GoodsModel(goods);

            // 构造 categoryList
            List<Category> categoryList = new List<Category>();

            // 先根据 goodsId 获取 该 goods对应的分类Id列表

            // 再 找到对应的分类列表
            foreach (var item in _goodsCategoryBLL.GetGoodsCategoryListByGoodsId(gm.Id))
            {
                Category category = _categoryBLL.GetCategoryById(item.CategoryId);
                categoryList.Add(category);
            }

            // 当前已选分类
            ViewData["CurrentCategoryList"] = categoryList;

            // 所有的分类
            ViewData["CategoryList"] = _categoryBLL.GetAllCategory();

            GoodsImage gi = new GoodsImage();
            gi = _goodsImagesBLL.GetPictureByGoodsId(gm.Id);

            gm.PicturePath = gi.ImagePath;

            return View(gm);
        }

        [HttpPost]
        public ActionResult Update(GoodsModel model)
        {
            if (model == null)
            {
                return RedirectToAction("List", new { msg = "更新失败" });
            }
            // 更新 Goods 本身
            #region 更新goods本身        
            Goods goods = _goodsBLL.GetGoodsByCode(model.Code);
            goods.Price = model.Price;
            goods.Description = model.Description;
            goods.ServiceCount = model.ServiceCount;

            // 商品添加新的字段 OriginalPrice 更新时 也要更新
            goods.OriginalPrice = model.OriginalPrice;

            if (!_goodsBLL.Update(goods))
            {
                return RedirectToAction("List", new { msg = "更新商品失败" });
            }
            #endregion

            // 更新 商品图片
            #region 更新商品图片

            GoodsImage gi = _goodsImagesBLL.GetPictureByGoodsId(goods.Id);
            if (gi.ImagePath != model.PicturePath)
            {
                gi.ImagePath = model.PicturePath;
            }
            if (!_goodsImagesBLL.Update(gi))
            {
                return RedirectToAction("List", new { msg = "更新商品图片失败" });
            }

            #endregion

            // 更新商品分类的时候 先删除原先的分类

            // 再根据新的 model.CategoryList 重新添加上分类

            #region 更新 商品分类列表 
            if (_goodsCategoryBLL.RemoveGoodsCategoryByGoodsId(goods.Id))
            {
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
            }
            #endregion

            return RedirectToAction("List", new { msg = "更新成功" });
        }

        /// <summary>
        /// 设置商品为推荐商品
        /// </summary>
        /// <param name="goodsCode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetRecommend(string goodsCode)
        {
            // 先判断传递的参数是否为空
            if (string.IsNullOrEmpty(goodsCode))
            {
                return Json("NullParam", JsonRequestBehavior.AllowGet);
            }
            // 再用 Code 来获取 goods 对象
            Goods goods = _goodsBLL.GetGoodsByCode(goodsCode);
            if (goods == null)
            {
                return Json("ErrorParam", JsonRequestBehavior.AllowGet);
            }

            // 判断是否已经是推荐商品了
            // 如果是 则删掉它
            if (_recommendBLL.IsRecommend(goods.Id))
            {
                if (_recommendBLL.Remove(goods.Id))
                {
                    return Json("True", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("False", JsonRequestBehavior.AllowGet);
                }
            }
            // 不是 则新加
            else
            {
                ReCommend rec = new ReCommend();
                rec.Id = Guid.NewGuid();
                rec.IsDeleted = false;
                rec.GoodsId = goods.Id;
                rec.DeletedTime = DateTime.MinValue.AddHours(8);
                rec.CreatedTime = DateTime.Now.Date;

                if (_recommendBLL.Add(rec))
                {
                    return Json("True", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("False", JsonRequestBehavior.AllowGet);
                }

            }


        }

        [HttpPost]
        public ActionResult Delete(string GoodsCode)
        {
            if (string.IsNullOrEmpty(GoodsCode))
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            Goods goods = _goodsBLL.GetGoodsByCode(GoodsCode);

            if (goods == null)
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            goods.IsDeleted = true;
            goods.DeletedTime = DateTime.Now;

            if (_goodsBLL.Update(goods))
            {
                // 删除商品图片
                GoodsImage gi = _goodsImagesBLL.GetPictureByGoodsId(goods.Id);
                gi.IsDeleted = true;
                gi.DeletedTime = DateTime.Now;

                _goodsImagesBLL.Update(gi);

                // 获取商品分类列表
                List<GoodsCategory> gcList = _goodsCategoryBLL.GetGoodsCategoryListByGoodsId(goods.Id).ToList();

                // 异步删除商品分类列表
                DelGoodsCategoryDel del = new DelGoodsCategoryDel(DeleteGoodsCategory);
                IAsyncResult result = del.BeginInvoke(gcList, CallBack, null);

                return Json("True", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
        }

        private void DeleteGoodsCategory(List<GoodsCategory> gcList)
        {
            foreach (var gc in gcList)
            {
                gc.IsDeleted = true;
                gc.DeletedTime = DateTime.Now;

                bool result = _goodsCategoryBLL.Update(gc);

                LogHelper.Log.Write("商品分类处理结果: " + result);
            }
        }

        private void CallBack(IAsyncResult ar)
        {
            try
            {
                DelGoodsCategoryDel del = (DelGoodsCategoryDel)ar.AsyncState;
                del.EndInvoke(ar);
            }
            catch (Exception)
            {

            }

        }
    }
}