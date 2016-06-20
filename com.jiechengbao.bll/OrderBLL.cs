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
        public int GetNewOrdersCount()
        {
            return _orderDAL.SelectOrderByDate(DateTime.Now.AddDays(-1).Date).Count();
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
        /// 获取昨天新提交的订单
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Order> GetYesterDayOrders()
        {
            return _orderDAL.SelectOrderByDate(DateTime.Now.AddDays(-1).Date);
        }
    }
}
