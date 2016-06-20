using com.jiechengbao.admin.Models;
using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.admin.Controllers
{
    public class HomeController:Controller
    {
        private IMemberBLL _memberBLL;
        private IOrderBLL _orderBLL;

        public HomeController(IMemberBLL memberBLL, IOrderBLL orderBLL)
        {
            _memberBLL = memberBLL;
            _orderBLL = orderBLL;
        }        

        /// <summary>
        /// 管理员后台首页 
        /// 登陆成功后进入此页
        /// 显示 各种数据信息 包括 当前用户数量 当前新提交的订单数量 
        /// 这里用图表表示
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.MemberCount = _memberBLL.GetAllNoDeletedMembersCount();
            IEnumerable<Member> memberList = _memberBLL.GetNewMembersAtYesterDay();
            ViewData["MemberList"] = memberList;

            // 获取昨天新提交的订单
            IEnumerable<Order> orderList = _orderBLL.GetYesterDayOrders();

            // 昨天新提交的订单的数量
            ViewBag.OrderCount = orderList.Count();

            List<OrderModel> orderModelList = new List<OrderModel>();

            // 构造OrderModelList OrderModel 包含MemberName
            foreach (var item in orderList)
            {
                OrderModel om = new OrderModel(item);
                // 主要是为了显示 MemberName
                om.MemberName = _memberBLL.GetMemberById(item.MemberId).NickeName;

                orderModelList.Add(om);
            }

            ViewData["OrderList"] = orderModelList;

            return View();
        }
    }
}