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
    public class AddressController:Controller
    {
        private IMemberBLL _memberBLL;
        private IAddressBLL _addressBLL;

        public AddressController(IMemberBLL memberBLL, IAddressBLL addressBLL)
        {
            _memberBLL = memberBLL;
            _addressBLL = addressBLL;
        }

        public ActionResult Editor(Guid Id)
        {
            Address address = _addressBLL.GetAddressById(Id);
            return View(address);
        }

        [HttpPost]
        public ActionResult Editor(AddressModel model, Guid Id)
        {
            Address address = _addressBLL.GetAddressById(Id);
            address.City = model.City;
            address.Consignee = model.Consignee;
            address.County = model.County;
            address.Detail = model.Detail;
            address.Phone = model.Phone;
            address.Province = model.Province;

            if (_addressBLL.Update(address))
            {
                return Json("True", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
        }

        
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(AddressModel model)
        {
            if (model == null)
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
            Address address = new Address();
            address.Id = Guid.NewGuid();
            address.CreatedTime = DateTime.Now.Date;
            address.IsDeleted = false;
            address.DeletedTime = DateTime.MinValue.AddHours(8);
            address.Consignee = model.Consignee;
            address.Phone = model.Phone;
            address.Province = model.Province;
            address.City = model.City;
            address.County = model.County;
            address.Detail = model.Detail;

            address.MemberId = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"] as string).Id;

            if (_addressBLL.Add(address))
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