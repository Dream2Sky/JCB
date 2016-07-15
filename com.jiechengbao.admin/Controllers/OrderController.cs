using com.jiechengbao.admin.Models;
using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.admin.Controllers
{
    public class OrderController:Controller
    {
        private IOrderBLL _orderBLL;
        private IMemberBLL _memberBLL;
        private IAddressBLL _addressBLL;
        private IOrderStatusBLL _orderStatusBLL;
        public OrderController(IOrderBLL orderBLL, 
            IMemberBLL memberBLL, IAddressBLL addressBLL,
            IOrderStatusBLL orderStatusBLL)
        {
            _orderBLL = orderBLL;
            _memberBLL = memberBLL;
            _addressBLL = addressBLL;
            _orderStatusBLL = orderStatusBLL;
        }

        /// <summary>
        /// 获得已完成的订单
        /// </summary>
        /// <returns></returns>
        
        public ActionResult Completed()
        {
            List<OrderModel> orderModelList = new List<OrderModel>();
            IEnumerable<Order> orderList = _orderBLL.GetCompletedOrders();

            foreach (var item in orderList)
            {
                OrderModel om = new OrderModel(item);
                Member member = _memberBLL.GetMemberById(item.MemberId);
                om.MemberName = member.NickeName;

                Address address = _addressBLL.GetAddressById(om.AddressId);
                om.Phone = address.Phone;
                om.Address = address.Province + "," + address.City + "," + address.County + "," + address.Detail;
                om.Consignee = address.Consignee;
                
                // 添加配送状态
                om.LogisticalStatus = _orderStatusBLL.GetOrderStatusByOrderId(om.Id).Status;

                orderModelList.Add(om);
            }
            ViewData["CompletedOrderList"] = orderModelList; 

            return View();
        }

        /// <summary>
        /// 搜索订单
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Completed(string condition)
        {
            List<OrderModel> orderModelList = new List<OrderModel>();
            List<Order> orderList = new List<Order>();
            // 偷懒 只按订单号查询
            orderList.Add(_orderBLL.GetOrderByOrderNo(condition));

            foreach (var item in orderList)
            {
                OrderModel om = new OrderModel(item);
                Member member = _memberBLL.GetMemberById(item.MemberId);
                om.MemberName = member.NickeName;

                Address address = _addressBLL.GetAddressById(om.AddressId);
                om.Phone = address.Phone;
                om.Address = address.Province + "," + address.City + "," + address.County + "," + address.Detail;
                om.Consignee = address.Consignee;
                
                // 添加配送地址
                om.LogisticalStatus = _orderStatusBLL.GetOrderStatusByOrderId(om.Id).Status;

                orderModelList.Add(om);
            }
            ViewData["CompletedOrderList"] = orderModelList;

            return View();
        }

        /// <summary>
        ///  修改订单的配送状态  后台的
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Shipping(string orderNo)
        {
            if (string.IsNullOrEmpty(orderNo))
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
            Order order = _orderBLL.GetOrderByOrderNo(orderNo);

            if (order == null)
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
            OrderStatus os = _orderStatusBLL.GetOrderStatusByOrderId(order.Id);

            if (os == null)
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            os.Status = 1;
            if (_orderStatusBLL.Update(os))
            {
                return Json("True", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 获得未完成的订单
        /// </summary>
        /// <returns></returns>
        public ActionResult UnCompleted()
        {
            List<OrderModel> orderModelList = new List<OrderModel>();
            IEnumerable<Order> orderList = _orderBLL.GetUnCompletedOrders();

            foreach (var item in orderList)
            {
                OrderModel om = new OrderModel(item);
                Member member = _memberBLL.GetMemberById(item.MemberId);
                om.MemberName = member.NickeName;

                Address address = _addressBLL.GetAddressById(om.AddressId);
                om.Phone = address.Phone;
                om.Address = address.Province + "," + address.City + "," + address.County + "," + address.Detail;
                om.Consignee = address.Consignee;

                //// 添加配送状态
                //om.LogisticalStatus = _orderStatusBLL.GetOrderStatusByOrderId(om.Id).Status;

                orderModelList.Add(om);

            }
            ViewData["CompletedOrderList"] = orderModelList;
            return View();
        }

        [HttpPost]
        public ActionResult UnCompleted(string condition)
        {
            List<OrderModel> orderModelList = new List<OrderModel>();
            List<Order> orderList = new List<Order>();

            // 又偷懒了
            orderList.Add(_orderBLL.GetOrderByOrderNo(condition));
            
            foreach (var item in orderList)
            {
                OrderModel om = new OrderModel(item);
                Member member = _memberBLL.GetMemberById(item.MemberId);
                om.MemberName = member.NickeName;

                Address address = _addressBLL.GetAddressById(om.AddressId);
                om.Phone = address.Phone;
                om.Address = address.Province + "," + address.City + "," + address.County + "," + address.Detail;
                om.Consignee = address.Consignee;

                // 添加配送状态
                //om.LogisticalStatus = _orderStatusBLL.GetOrderStatusByOrderId(om.Id).Status;

                orderModelList.Add(om);

            }
            ViewData["CompletedOrderList"] = orderModelList;
            return View();
        }
    }
}