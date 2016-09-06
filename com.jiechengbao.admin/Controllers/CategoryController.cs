using com.jiechengbao.Ibll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using com.jiechengbao.entity;
using com.jiechengbao.common;
using POPO.ActionFilter.Helper;
using com.jiechengbao.admin.Global;

namespace com.jiechengbao.admin.Controllers
{
    [WhitespaceFilter]
    [ETag]
    [IsLogin]
    public class CategoryController : Controller
    {
        private ICategoryBLL _categoryBLL;
        private IGoodsCategoryBLL _goodsCategoryBLL;
        public CategoryController(ICategoryBLL categoryBLL, IGoodsCategoryBLL goodsCategoryBLL)
        {
            _categoryBLL = categoryBLL;
            _goodsCategoryBLL = goodsCategoryBLL;
        }    
        public ActionResult List(string msg)
        {
            ViewBag.Msg = msg;
            CategoryList();
            return View();
        }
        public PartialViewResult CategoryList()
        {
            ViewData["CategoryList"] = _categoryBLL.GetAllCategory();
            return PartialView();
        }
        [HttpPost]
        public ActionResult Add(string CategoryName, bool IsService)
        {
            // 判断新添加的categoryName 是否为空
            if (string.IsNullOrEmpty(CategoryName))
            {
                return Json("NullObject", JsonRequestBehavior.AllowGet);
            }

            Category category = new Category();
            category.Id = Guid.NewGuid();
            category.CategoryNO = "Category_" + TimeManager.GetCurrentTimestamp();
            category.CreatedTime = DateTime.Now.Date;
            category.IsDeleted = false;
            category.Name = CategoryName;
            category.IsService = IsService;

            // 判断是否存在新添加的categoryName
            if (_categoryBLL.IsExist(CategoryName))
            {
                return Json("ExistObject", JsonRequestBehavior.AllowGet);
            }

            // 添加新Category
            if (_categoryBLL.Add(category))
            {
                // 添加成功
                return Json("True", JsonRequestBehavior.AllowGet);
            }
            else
            {
                // 添加失败
                return Json("False", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Editor()
        {
            ViewData["CategoryList"] = _categoryBLL.GetAllCategory();
            return View();
        }
        [HttpPost]
        public ActionResult Update(string categoryNO, string categoryName, bool isService)
        {
            Category category = _categoryBLL.GetCategoryByCategoryNo(categoryNO);
            if (category == null)
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
            else if (string.IsNullOrEmpty(categoryName))
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
            else
            {
                category.Name = categoryName;
                category.IsService = isService;
                if (_categoryBLL.Save(category))
                {
                    var obj = new { code = categoryNO, name = categoryName, isService = isService };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("False", JsonRequestBehavior.AllowGet);
                }
            }
        }
        [HttpPost]
        public ActionResult Delete(string categoryNO)
        {
            Category category = _categoryBLL.GetCategoryByCategoryNo(categoryNO);
            List<GoodsCategory> gcList = _goodsCategoryBLL.GetGoodsCategoryListByCategoryId(category.Id).ToList();

            bool res = false;

            using (JCB_DBContext db = new JCB_DBContext ())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        category.IsDeleted = true;
                        db.Entry(category).State = System.Data.Entity.EntityState.Modified;

                        foreach (var item in gcList)
                        {
                            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
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

            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}