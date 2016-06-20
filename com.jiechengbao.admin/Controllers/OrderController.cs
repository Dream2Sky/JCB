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
        public OrderController(IOrderBLL orderBLL, IMemberBLL memberBLL)
        {
            _orderBLL = orderBLL;
            _memberBLL = memberBLL;
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
                om.MemberName = _memberBLL.GetMemberById(item.MemberId).NickeName;

                orderModelList.Add(om);
            }
            ViewData["CompletedOrderList"] = orderModelList; 

            return View();
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
                om.MemberName = _memberBLL.GetMemberById(item.MemberId).NickeName;

                orderModelList.Add(om);
            }
            ViewData["CompletedOrderList"] = orderModelList;
            return View();
        }
    }
}