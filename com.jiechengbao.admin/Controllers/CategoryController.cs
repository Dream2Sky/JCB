using com.jiechengbao.Ibll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using com.jiechengbao.entity;
using com.jiechengbao.common;

namespace com.jiechengbao.admin.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryBLL _categoryBLL;
        public CategoryController(ICategoryBLL categoryBLL)
        {
            _categoryBLL = categoryBLL;
        }

        public ActionResult List(string msg)
        {
            ViewBag.Msg = msg;
            ViewData["CategoryList"] = _categoryBLL.GetAllCategory();
            return View();
        }

        [HttpPost]
        public ActionResult Add(string categoryName, int isService)
        {
            // 判断新添加的categoryName 是否为空
            if (string.IsNullOrEmpty(categoryName))
            {
                return RedirectToAction("List", new { msg = "分类名不能为空" });
            }

            Category category = new Category();
            category.Id = Guid.NewGuid();
            category.CategoryNO = "Category_" + TimeManager.GetCurrentTimestamp();
            category.CreatedTime = DateTime.Now.Date;
            category.IsDeleted = false;
            category.Name = categoryName;
            category.IsService = isService == 1 ? true : false;

            // 判断是否存在新添加的categoryName
            if (_categoryBLL.IsExist(categoryName))
            {
                return RedirectToAction("List", new { msg = "该分类已存在，请重新添加" });
            }

            // 添加新Category
            if (_categoryBLL.Add(category))
            {
                // 添加成功
                return RedirectToAction("List", new { msg = "添加成功" });
            }
            else
            {
                return RedirectToAction("List", new { msg = "添加失败" });
            }

        }

        public ActionResult Editor()
        {
            ViewData["CategoryList"] = _categoryBLL.GetAllCategory();
            return View();
        }

        [HttpPost]
        public ActionResult Update(string categoryNO, string categoryName, int isService)
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
                category.IsService = isService == 1 ? true : false;
                if (_categoryBLL.Save(category))
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
        public ActionResult Delete(string categoryNO)
        {
            Category category = _categoryBLL.GetCategoryByCategoryNo(categoryNO);
            if (category == null)
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
            else if(_categoryBLL.Delete(category))
            {
                return Json("True", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
        }
    }
}