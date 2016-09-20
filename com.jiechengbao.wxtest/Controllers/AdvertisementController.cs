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



    }
}