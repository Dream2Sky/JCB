using com.jiechengbao.admin.Models;
using com.jiechengbao.common;
using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.admin.Controllers
{
    public class AdvertisementController : Controller
    {
        private IAdvertisementBLL _advertisementBLL;
        private ICategoryBLL _categoryBLL;
        public AdvertisementController(IAdvertisementBLL advertisementBLL, ICategoryBLL categoryBLL)
        {
            _advertisementBLL = advertisementBLL;
            _categoryBLL = categoryBLL;
        }

        public ActionResult List()
        {
            AdList();
            return View();
        }

        public PartialViewResult AdList()
        {
            ViewData["AdModelList"] = _advertisementBLL.GetAllNotDeletedAdvertisements();
            return PartialView();
        }

        [HttpPost]
        public ActionResult Add(AdModel ad)

        {
            bool res = false;
            string msg = string.Empty;

            if (ad == null)
            {
                var objNull = new
                {
                    code = res,
                    msg = "参数提交发生错误"
                };
                return Json(objNull, JsonRequestBehavior.AllowGet);
            }

            #region 创建事务 添加广告
            using (JCB_DBContext db = new JCB_DBContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        Advertisement advertisement = new Advertisement();

                        advertisement.Id = Guid.NewGuid();
                        advertisement.AdCode = "AdCode_" + TimeManager.GetCurrentTimestamp();
                        advertisement.AdDescription = ad.AdDescription;
                        advertisement.AdImagePath = ad.AdImagePath;
                        advertisement.AdName = ad.AdName;
                        advertisement.CreatedTime = DateTime.Now;
                        advertisement.IsDeleted = false;
                        advertisement.DeletedTime = DateTime.MinValue.AddHours(8);
                        advertisement.IsRecommend = ad.IsRecommand;

                        Category category = new Category();
                        category = _categoryBLL.GetCategoryByCategoryNo(ad.Category);

                        advertisement.CategoryId = category.Id;

                        db.Advertisements.Add(advertisement);
                        db.SaveChanges();

                        trans.Commit();
                        res = true;
                        msg = "添加成功";
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Log.Write(ex.Message);
                        LogHelper.Log.Write(ex.StackTrace);

                        trans.Rollback();
                        res = false;
                        msg = "添加失败";
                    }
                }
            }

            #endregion

            var obj = new
            {
                code = res,
                msg = msg
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(AdModel ad)
        {
            var res = false;
            var msg = string.Empty;

            if (ad == null)
            {
                var objNUll = new
                {
                    code = res,
                    msg = "参数提交错误"
                };
                return Json(objNUll, JsonRequestBehavior.AllowGet);
            }

            #region 启动事务 更新广告    
            using (JCB_DBContext db = new JCB_DBContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        Advertisement advertisement = new Advertisement();
                        advertisement = _advertisementBLL.GetAdByAdCode(ad.AdCode);

                        advertisement.AdDescription = ad.AdDescription;
                        advertisement.AdImagePath = ad.AdImagePath;
                        advertisement.AdName = ad.AdName;
                        advertisement.IsRecommend = ad.IsRecommand;

                        Category category = new Category();
                        category = _categoryBLL.GetCategoryByCategoryNo(ad.AdCode);

                        advertisement.CategoryId = category.Id;

                        db.Set<Advertisement>().Attach(advertisement);
                        db.Entry(advertisement).State = System.Data.Entity.EntityState.Modified;

                        db.SaveChanges();

                        trans.Commit();
                        res = true;
                        msg = "修改成功";
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Log.Write(ex.Message);
                        LogHelper.Log.Write(ex.StackTrace);

                        trans.Rollback();
                        res = false;
                        msg = "修改失败";
                    }
                }
            }
            #endregion

            var obj = new
            {
                code = res,
                msg = msg
            };

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(string adCode)
        {
            bool res = false;
            string msg = string.Empty;
            if (string.IsNullOrEmpty(adCode))
            {
                var objNull = new
                {
                    code = res,
                    msg = "参数提交错误"
                };
                return Json(objNull, JsonRequestBehavior.AllowGet);
            }

            using (JCB_DBContext db = new JCB_DBContext ())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        Advertisement ad = new Advertisement();
                        ad = db.Set<Advertisement>().SingleOrDefault(n => n.AdCode == adCode);

                        ad.IsDeleted = true;
                        ad.DeletedTime = DateTime.Now;

                        db.Set<Advertisement>().Attach(ad);
                        db.Entry(ad).State = System.Data.Entity.EntityState.Deleted;

                        db.SaveChanges();
                        trans.Commit();

                        res = true;
                        msg = "删除成功";
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Log.Write(ex.Message);
                        LogHelper.Log.Write(ex.StackTrace);
                        trans.Rollback();

                        res = false;
                        msg = "删除失败";
                    }
                }
            }

            var obj = new
            {
                code = res,
                msg = msg
            };

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }

}