using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Ibll
{
    public interface IOrderBLL
    {
        int GetNewOrdersCount();

        IEnumerable<Order> GetYesterDayOrders();

        IEnumerable<Order> GetCompletedOrders();
        IEnumerable<Order> GetUnCompletedOrders();

    }
}
