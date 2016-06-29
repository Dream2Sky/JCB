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

        public ActionResult List()
        {
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());
            List<Address> addressList = _addressBLL.GetAddressByMemberId(member.Id).ToList();

            ViewData["AddressList"] = addressList;
            return View();
        }

        [HttpPost]
        public ActionResult SetDefault(string addressId)
        {
            // 上来就要判断传递的 ID 是否为空
            if (string.IsNullOrEmpty(addressId))
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            // 接着看能不能根据这个Id 获取到正确的 Address 对象
            Address address = _addressBLL.GetAddressById(Guid.Parse(addressId));
            if (address == null)
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());

            List<Address> addressList = _addressBLL.GetAddressByMemberId(member.Id).ToList();

            foreach (var item in addressList.Where(n=>n.Id != address.Id))
            {
                item.IsDefault = false;

                if (!_addressBLL.Update(item))
                {
                    // 这里直接返回 是不影响后面配送地址的获取逻辑的

                    // 所以这里就没有必要做回滚操作
                    return Json("False", JsonRequestBehavior.AllowGet);
                }
            }

            address.IsDefault = true;

            if (!_addressBLL.Update(address))
            {
                // 同上
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            return Json("True", JsonRequestBehavior.AllowGet);
        }
    }

}