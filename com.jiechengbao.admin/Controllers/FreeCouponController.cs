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
    public class FreeCouponController:Controller
    {
        private IFreeCouponBLL _freeCouponBLL;
        public FreeCouponController(IFreeCouponBLL freeCouponBLL)
        {
            _freeCouponBLL = freeCouponBLL;
        }

        public ActionResult List()
        {
            FreeCouponList();
            return View();
        }

        public PartialViewResult FreeCouponList()
        {
            List<FreeCoupon> FreeCouponList = new List<FreeCoupon>();

            FreeCouponList = _freeCouponBLL.GetAllNotDeletedCoupon().ToList();
            ViewData["FreeCouponList"] = FreeCouponList;
            return PartialView(); 
        }

        [HttpPost]
        public ActionResult Add(FreeCoupon fc)
        {
            if (fc == null)
            {
                return Json("NullObject", JsonRequestBehavior.AllowGet);
            }

            fc.Id = Guid.NewGuid();
            fc.CouponCode = "CouponCode_" + TimeManager.GetCurrentTimestamp();
            fc.CreatedTime = DateTime.Now;
            fc.DeletedTime = DateTime.MinValue.AddHours(8);

            if (_freeCouponBLL.IsExist(fc))
            {
                return Json("ExistObject", JsonRequestBehavior.AllowGet);
            }

            if (_freeCouponBLL.Add(fc))
            {
                return Json("True", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Update(FreeCoupon fc)
        {
            if (fc == null)
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            FreeCoupon freeCoupon = _freeCouponBLL.GetFreeCouponByCode(fc.CouponCode);

            freeCoupon.CouponName = fc.CouponName;
            freeCoupon.Price = fc.Price;
            freeCoupon.Description = fc.Description;

            if (_freeCouponBLL.Update(freeCoupon))
            {
                var obj = new
                {
                    code = fc.CouponCode,
                    name = freeCoupon.CouponName,
                    desc = freeCoupon.Description,
                    price = freeCoupon.Price
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete(string freeCouponCode)
        {
            if (string.IsNullOrEmpty(freeCouponCode))
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            FreeCoupon fc = _freeCouponBLL.GetFreeCouponByCode(freeCouponCode);
            if (fc == null)
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            fc.IsDeleted = true;
            if (_freeCouponBLL.Update(fc))
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