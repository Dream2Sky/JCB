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
using MySql.Data.MySqlClient;

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

                GoodsImage gi = _goodsImagesBLL.GetPictureByGoodsId(item.Id);
                if (gi == null)
                {
                    continue;
                }
                gm.PicturePath = gi.ImagePath;
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
                return Json("EmptyModel", JsonRequestBehavior.AllowGet);
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

            // 增加商品的积分兑换项  另一种货币

            // 任何商品可以通过积分购买

            goods.ExchangeCredit = model.ExchangeCredit;

            // 商品新家字段  OriginalPrice  原价  

            // 该字段不影响原来商品添加的逻辑
            goods.OriginalPrice = model.OriginalPrice;

            // 添加成功后 构造 goodsimage 对象
            // 并添加到数据库
            GoodsImage gi = new GoodsImage();
            gi.Id = Guid.NewGuid();
            gi.ImagePath = model.PicturePath;
            gi.CreatedTime = DateTime.Now.Date;
            gi.GoodsId = goods.Id;
            gi.IsDeleted = false;
            gi.DeletedTime = DateTime.MinValue.AddHours(8);

            List<GoodsCategory> gcList = new List<GoodsCategory>();

            foreach (var item in model.CategoryList)
            {
                // 构造 GoodsCategory 对象
                GoodsCategory gc = new GoodsCategory();
                gc.Id = Guid.NewGuid();
                gc.CreatedTime = DateTime.Now.Date;
                gc.CategoryId = _categoryBLL.GetCategoryByCategoryNo(item).Id;
                gc.IsDeleted = false;
                gc.GoodsId = goods.Id;
                gc.DeletedTime = DateTime.MinValue.AddHours(8);

                gcList.Add(gc);
            }

            // 使用事务添加新的商品
            if (AddGoodsWithTransaction(goods, gi, gcList))
            {
                return Json("True", JsonRequestBehavior.AllowGet);
            }
            return Json("False", JsonRequestBehavior.AllowGet);

            #endregion
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
            ViewData["CategoryList"] = _categoryBLL.GetAllCategory().ToList();

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
                return Json("NullData",JsonRequestBehavior.AllowGet);
            }

            // 更新 Goods 本身
            #region 更新goods本身        
            Goods goods = _goodsBLL.GetGoodsByCode(model.Code);

            if (goods == null)
            {
                return Json("NullObject", JsonRequestBehavior.AllowGet);
            }

            goods.Name = model.Name;
            goods.Price = model.Price;
            goods.Description = model.Description;
            goods.ServiceCount = model.ServiceCount;

            // 商品添加新的消费字段 exchangeCredit 商品可以用积分购买
            goods.ExchangeCredit = model.ExchangeCredit;

            // 商品添加新的字段 OriginalPrice 更新时 也要更新
            goods.OriginalPrice = model.OriginalPrice;

            GoodsImage gi = _goodsImagesBLL.GetPictureByGoodsId(goods.Id);

            if (gi == null)
            {
                return Json("NullObject", JsonRequestBehavior.AllowGet);
            }

            List<GoodsCategory> gcList = new List<GoodsCategory>();

            foreach (var item in model.CategoryList)
            {
                GoodsCategory gc = new GoodsCategory();
                gc.Id = Guid.NewGuid();
                gc.CreatedTime = DateTime.Now.Date;
                gc.CategoryId = _categoryBLL.GetCategoryByCategoryNo(item).Id;
                gc.IsDeleted = false;
                gc.GoodsId = goods.Id;

                gcList.Add(gc);
            }

            bool res = false;
            using (JCB_DBContext db = new JCB_DBContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    if (gi.ImagePath != model.PicturePath)
                    {
                        gi.ImagePath = model.PicturePath;

                        res = UpdateGoodsWithTransaction(db, trans, goods, gi, gcList);
                    }
                    else
                    {
                        res = UpdateGoodsWithTransaction(db, trans, goods, gcList);
                    }
                }
            }


            return Json(res, JsonRequestBehavior.AllowGet);

            #endregion
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

        [NonAction]
        private bool AddGoodsWithTransaction(Goods goods, GoodsImage gi, List<GoodsCategory> gcList)
        {
            bool res = false;
            using (JCB_DBContext db = new JCB_DBContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Set<Goods>().Add(goods);
                        db.GoodsImages.Add(gi);
                        foreach (var item in gcList)
                        {
                            db.Set<GoodsCategory>().Add(item);
                        }

                        db.SaveChanges();

                        trans.Commit();
                        res = true;

                    }
                    catch (Exception ex)
                    {
                        LogHelper.Log.Write(ex.Message);
                        LogHelper.Log.Write(ex.StackTrace);
                        trans.Rollback();
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// 
        /// 使用事务更新商品
        /// </summary>
        /// <param name="goods"></param>
        /// <param name="gi"></param>
        /// <param name="imagePath"></param>
        /// <param name="gcList"></param>
        /// <returns></returns>
        [NonAction]
        private bool UpdateGoodsWithTransaction(JCB_DBContext db, System.Data.Entity.DbContextTransaction trans, Goods goods, GoodsImage gi, List<GoodsCategory> gcList)
        {
            bool res = false;
            try
            {
                // 更新商品图片数据库状态
                db.Set<GoodsImage>().Attach(gi);
                db.Entry(gi).State = System.Data.Entity.EntityState.Modified;

                res = UpdateGoodsWithTransaction(db, trans, goods, gcList);
                
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);

                trans.Rollback();
                
            }
            return res;
        }

        private bool UpdateGoodsWithTransaction(JCB_DBContext db, System.Data.Entity.DbContextTransaction trans, Goods goods, List<GoodsCategory> gcList)
        {
            bool res = false;
            try
            {
                db.Set<Goods>().Attach(goods);
                db.Entry(goods).State = System.Data.Entity.EntityState.Modified;

                if (!_goodsCategoryBLL.RemoveGoodsCategoryByGoodsId(goods.Id))
                {
                    throw new Exception("删除 goods.Id = " + goods.Id + " 的goodsCategory列表失败");
                }

                foreach (var item in gcList)
                {
                    db.Set<GoodsCategory>().Attach(item);
                    db.Set<GoodsCategory>().Add(item);
                }

                db.SaveChanges();

                trans.Commit();
                res = true;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);

                trans.Rollback();
            }
            return res;
        }
    }
}