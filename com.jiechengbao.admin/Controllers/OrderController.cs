﻿using com.jiechengbao.admin.Global;
using com.jiechengbao.admin.Models;
using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using POPO.ActionFilter.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.admin.Controllers
{
    [WhitespaceFilter]
    [ETag]
    [IsLogin]
    public class OrderController : Controller
    {
        private IOrderBLL _orderBLL;
        private IMemberBLL _memberBLL;
        private IAddressBLL _addressBLL;
        private IOrderStatusBLL _orderStatusBLL;
        private IOrderDetailBLL _orderDetailBLL;
        private IGoodsBLL _goodsBLL;
        public OrderController(IOrderBLL orderBLL,
            IMemberBLL memberBLL, IAddressBLL addressBLL,
            IOrderStatusBLL orderStatusBLL,
            IOrderDetailBLL orderDetailBLL, IGoodsBLL goodsBLL)
        {
            _orderBLL = orderBLL;
            _memberBLL = memberBLL;
            _addressBLL = addressBLL;
            _orderStatusBLL = orderStatusBLL;
            _orderDetailBLL = orderDetailBLL;
            _goodsBLL = goodsBLL;
        }

        /// <summary>
        /// 获得已完成的订单
        /// </summary>
        /// <returns></returns>

        public ActionResult Completed()
        {
            List<OrderModel> orderModelList = new List<OrderModel>();
            IEnumerable<Order> orderList = _orderBLL.GetCompletedOrders();

            orderList = orderList.OrderBy(n => n.IsCompleted);

            foreach (var item in orderList)
            {
                OrderModel om = new OrderModel(item);
                Member member = _memberBLL.GetMemberById(item.MemberId);
                if (member == null)
                {
                    continue;
                }
                om.MemberName = member.NickeName;
                foreach (var goods in _orderDetailBLL.GetOrderDetailByOrderNo(om.OrderNo))
                {
                    om.GoodsNameList += _goodsBLL.GetGoodsById(goods.GoodsId).Name + ",";
                }

                // 没有了配送系统 
                // 所以这段代码就没用了

                //Address address = _addressBLL.GetAddressById(om.AddressId);
                //om.Phone = address.Phone;
                //om.Address = address.Province + "," + address.City + "," + address.County + "," + address.Detail;
                //om.Consignee = address.Consignee;

                //// 添加配送状态
                //om.LogisticalStatus = _orderStatusBLL.GetOrderStatusByOrderId(om.Id).Status;

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

            orderList = orderList.OrderBy(n => n.IsCompleted).ToList();

            foreach (var item in orderList)
            {
                OrderModel om = new OrderModel(item);
                Member member = _memberBLL.GetMemberById(item.MemberId);
                if (member == null)
                {
                    continue;
                }
                om.MemberName = member.NickeName;
                foreach (var goods in _orderDetailBLL.GetOrderDetailByOrderNo(om.OrderNo))
                {
                    om.GoodsNameList += _goodsBLL.GetGoodsById(goods.GoodsId).Name + ",";
                }
                //Address address = _addressBLL.GetAddressById(om.AddressId);
                //om.Phone = address.Phone;
                //om.Address = address.Province + "," + address.City + "," + address.County + "," + address.Detail;
                //om.Consignee = address.Consignee;

                //// 添加配送地址
                //om.LogisticalStatus = _orderStatusBLL.GetOrderStatusByOrderId(om.Id).Status;

                orderModelList.Add(om);
            }
            ViewData["CompletedOrderList"] = orderModelList;

            return View();
        }



        #region 没有了配送系统 就不需要修改订单配送状态  所以此方法不用了


        /// <summary>
        ///  修改订单的配送状态  后台的
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        [HttpPost]
        [NonAction] // 此Action不用了 不能访问
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

        #endregion

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

                if (member == null)
                {
                    continue;
                }

                om.MemberName = member.NickeName;
                foreach (var goods in _orderDetailBLL.GetOrderDetailByOrderNo(om.OrderNo))
                {
                    om.GoodsNameList += _goodsBLL.GetGoodsById(goods.GoodsId).Name + ",";
                }

                //Address address = _addressBLL.GetAddressById(om.AddressId);
                //om.Phone = address.Phone;
                //om.Address = address.Province + "," + address.City + "," + address.County + "," + address.Detail;
                //om.Consignee = address.Consignee;

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

                if (member == null)
                {
                    continue;
                }

                om.MemberName = member.NickeName;

                foreach (var goods in _orderDetailBLL.GetOrderDetailByOrderNo(om.OrderNo))
                {
                    om.GoodsNameList += _goodsBLL.GetGoodsById(goods.GoodsId).Name + ",";
                }
                //Address address = _addressBLL.GetAddressById(om.AddressId);
                //om.Phone = address.Phone;
                //om.Address = address.Province + "," + address.City + "," + address.County + "," + address.Detail;
                //om.Consignee = address.Consignee;

                // 添加配送状态
                //om.LogisticalStatus = _orderStatusBLL.GetOrderStatusByOrderId(om.Id).Status;

                orderModelList.Add(om);

            }
            ViewData["CompletedOrderList"] = orderModelList;
            return View();
        }

        public JsonResult SetCompleted(string orderNo)
        {
            if (string.IsNullOrEmpty(orderNo))
            {
                return Json("Empty", JsonRequestBehavior.AllowGet);
            }

            string res = "False";

            using (JCB_DBContext db = new JCB_DBContext ())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        Order order = db.Set<Order>().Where(n => n.OrderNo == orderNo).Single();
                        order.IsCompleted = 1;

                        db.Set<Order>().Attach(order);
                        db.Entry(order).State = System.Data.Entity.EntityState.Modified;

                        db.SaveChanges();

                        trans.Commit();
                        res = "True";
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Log.Write(ex.Message);
                        LogHelper.Log.Write(ex.StackTrace);

                        trans.Rollback();
                        res = "False";
                    }
                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}