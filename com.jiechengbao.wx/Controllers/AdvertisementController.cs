using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using com.jiechengbao.wx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.wx.Controllers
{
    public class AdvertisementController:Controller
    {
        private IAdvertisementBLL _advertisementBLL;
        private ICategoryBLL _categoryBLL;
        public AdvertisementController(IAdvertisementBLL advertisementBLL, ICategoryBLL categoryBLL)
        {
            _advertisementBLL = advertisementBLL;
            _categoryBLL = categoryBLL;
        }

        /// <summary>
        /// 获取广告列表  根据不同的分类
        /// </summary>
        /// <param name="categoryCode"></param>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult AdList(string categoryCode)
        {
            #region 注释           
            //// 上来先判断传进来的categoryCode 是否为空
            //if (string.IsNullOrEmpty(categoryCode))
            //{
            //    // 如果是 则查出前10个广告
            //    List<AdModel> AdList = new List<AdModel>();

            //    foreach (var item in _advertisementBLL.GetAllNotDeletedAdvertisements(10))
            //    {
            //        if (item.IsDeleted == true)
            //        {
            //            continue;
            //        }
            //        AdModel ad = new Models.AdModel();
            //        ad.AdName = item.AdName;
            //        ad.AdImagePath = item.AdImagePath;
            //        ad.AdDescription = item.AdDescription;

            //        AdList.Add(ad);
            //    }

            //    ViewData["AdModelList"] = AdList;
            //}
            //else
            //{
            //    Category category = _categoryBLL.GetCategoryByCategoryNo(categoryCode);
            //    if (category == null)
            //    {
            //        ViewData["AdModelList"] = null;
            //    }
            //    else
            //    {
            //        List<AdModel> adList = new List<Models.AdModel>();
            //        foreach (var item in _advertisementBLL.GetNotDeletedAdvertisementsByCategory(category.Id))
            //        {
            //            AdModel ad = new Models.AdModel();

            //            ad.AdName = item.AdName;
            //            ad.AdImagePath = item.AdImagePath;
            //            ad.AdDescription = item.AdDescription;

            //            adList.Add(ad);
            //        }

            //        ViewData["AdModelList"] = adList;
            //    }
            //}
            #endregion
            List<AdModel> AdModelList = new List<AdModel>();
            List<AdModel> IsReCommandList = new List<AdModel>();
            List<AdModel> NotReCommandList = new List<AdModel>();

            // 上来先判断 传进来的 cateogryCode 是否为空
            if (string.IsNullOrEmpty(categoryCode))
            {
                #region 当 分类为空 时 获取广告列表
                
                // 获取未删除的推荐广告
                foreach (var item in _advertisementBLL.GetNotDeletedAndIsRecommandAd(5))
                {
                    AdModel ad = new AdModel();
                    ad.AdName = item.AdName;
                    ad.AdCode = item.AdCode;
                    ad.AdDescription = item.AdDescription;
                    ad.AdImagePath = item.AdImagePath;
                    ad.IsRecommand = item.IsRecommend;

                    IsReCommandList.Add(ad);
                }

                // 获取未删除的非推荐广告
                foreach (var item in _advertisementBLL.GetNotDeletedAndNotIsRecommandAd(10))
                {
                    AdModel ad = new AdModel();
                    ad.AdName = item.AdName;
                    ad.AdCode = item.AdCode;
                    ad.AdDescription = item.AdDescription;
                    ad.AdImagePath = item.AdImagePath;
                    ad.IsRecommand = item.IsRecommend;

                    NotReCommandList.Add(ad);
                }

                // 构造AdModelList
                int k = 0;
                for (int i = 0; i < IsReCommandList.Count; i++)
                {
                    AdModelList.Add(IsReCommandList[i]);
                    k += i;
                    AdModelList.Add(NotReCommandList[k]);
                    k += 1;
                    AdModelList.Add(NotReCommandList[k]);
                }

                AdModelList.AddRange(NotReCommandList.Skip(k));
                ViewData["AdModelList"] = AdModelList;

                #endregion
            }
            else
            {
                #region 获取指定分类的广告列表
                Category category = _categoryBLL.GetCategoryByCategoryNo(categoryCode);
                
                // 获取指定分类的未删除的推荐广告
                foreach (var item in _advertisementBLL.GetNotDeletedAndIsRecommandAdByCategory(category.Id))
                {
                    AdModel ad = new AdModel();
                    ad.AdName = item.AdName;
                    ad.AdCode = item.AdCode;
                    ad.AdImagePath = item.AdImagePath;
                    ad.AdDescription = item.AdDescription;
                    ad.IsRecommand = item.IsRecommend;

                    IsReCommandList.Add(ad);
                }

                // 获取指定分类的未删除的非推荐广告
                foreach (var item in _advertisementBLL.GetNotDeletedAndNotIsRecommandAdByCategory(category.Id))
                {
                    AdModel ad = new AdModel();
                    ad.AdName = item.AdName;
                    ad.AdCode = item.AdCode;
                    ad.AdImagePath = item.AdImagePath;
                    ad.AdDescription = item.AdDescription;
                    ad.IsRecommand = item.IsRecommend;

                    NotReCommandList.Add(ad);
                }

                // 构造 AdModelList
                int k = 0;
                for (int i = 0; i < AdModelList.Count; i++)
                {
                    AdModelList.Add(IsReCommandList[i]);
                    k += i;
                    AdModelList.Add(NotReCommandList[k]);
                    k += 1;
                    AdModelList.Add(NotReCommandList[k]);
                }

                AdModelList.AddRange(NotReCommandList.Skip(k));
                #endregion
            }

            ViewData["AdModelList"] = AdModelList;
            return PartialView();
        }

        public ActionResult AdDetail(string adCode)
        {
            AdModel ad = new AdModel();

            Advertisement advertisement = _advertisementBLL.GetAdByAdCode(adCode);
            if (advertisement == null)
            {
                ViewBag.Advertisement = advertisement;
                return View();
            }

            ad.AdCode = advertisement.AdCode;
            ad.AdDescription = advertisement.AdDescription;
            ad.AdImagePath = advertisement.AdImagePath;
            ad.AdName = advertisement.AdName;
            ad.IsRecommand = advertisement.IsRecommend;

            ViewBag.Advertisement = ad;

            return View();
        }

    }
}