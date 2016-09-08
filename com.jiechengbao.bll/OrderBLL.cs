using com.jiechengbao.Ibll;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.jiechengbao.entity;

namespace com.jiechengbao.bll
{
    public class OrderBLL : IOrderBLL
    {
        private IOrderDAL _orderDAL;
        public OrderBLL(IOrderDAL orderDAL)
        {
            _orderDAL = orderDAL;
        }

        public bool Add(Order order)
        {
            return _orderDAL.Insert(order);
        }

        /// <summary>
        /// 获取账户的所有订单
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public IEnumerable<Order> GetAllOrders(Guid memberId)
        {
            return _orderDAL.SelectAllByMemberId(memberId);
        }

        /// <summary>
        /// 获得已完成的订单
        /// order status
        /// 0 表示 未完成的订单
        /// 1  已完成的订单
        /// 2  已取消的订单
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Order> GetCompletedOrders()
        {
            return _orderDAL.SelectByStatus(1);
        }

        /// <summary>
        /// 获得指定用户的已完成的订单
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public IEnumerable<Order> GetCompletedOrders(Guid memberId)
        {
            return _orderDAL.SelectByStatus(1, memberId);
        }

        public int GetNewOrdersCount()
        {
            return _orderDAL.SelectOrderByDate(DateTime.Now.AddDays(-1).Date).Count();
        }

        public Order GetOrderByOrderId(Guid orderId)
        {
            return _orderDAL.SelectById(orderId);
        }

        /// <summary>
        /// 根据 订单号 获取订单
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public Order GetOrderByOrderNo(string orderNo)
        {
            return _orderDAL.SelectByOrderNo(orderNo);
        }

        /// <summary>
        /// 根据订单状态获取订单
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public IEnumerable<Order> GetOrdersByStatus(Guid memberId, int status)
        {
            return _orderDAL.SelectByStatus(status, memberId);
        }

        /// <summary>
        /// 获取未完成的订单 status = 0
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Order> GetUnCompletedOrders()
        {
            return _orderDAL.SelectByStatus(0);
        }

        /// <summary>
        /// 获得指定用户的未完成的订单
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public IEnumerable<Order> GetUnCompletedOrders(Guid memberId)
        {
            return _orderDAL.SelectByStatus(0, memberId);
        }

        /// <summary>
        /// 获取昨天新提交的订单
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Order> GetYesterDayOrders()
        {
            return _orderDAL.SelectOrderByDate(DateTime.Now.AddDays(-7).Date);
        }

        /// <summary>
        /// 判断是否有为完成的订单
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public bool HasUncompletedOrders(Guid memberId)
        {
            if (_orderDAL.SelectByStatus(0,memberId).Count()>0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 更新订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool Update(Order order)
        {
            return _orderDAL.Update(order);
        }
    }
}
