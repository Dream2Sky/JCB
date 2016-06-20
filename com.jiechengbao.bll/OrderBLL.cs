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
        public int GetNewOrdersCount()
        {
            return _orderDAL.SelectOrderByDate(DateTime.Now.AddDays(-1).Date).Count();
        }

        public IEnumerable<Order> GetYesterDayOrders()
        {
            return _orderDAL.SelectOrderByDate(DateTime.Now.AddDays(-1).Date);
        }
    }
}
