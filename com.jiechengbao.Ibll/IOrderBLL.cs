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
        bool Add(Order order);
        bool Update(Order order);
        Order GetOrderByOrderNo(string orderNo);
        IEnumerable<Order> GetCompletedOrders(Guid memberId);
        IEnumerable<Order> GetUnCompletedOrders(Guid memberId);
        bool HasUncompletedOrders(Guid memberId);

        IEnumerable<Order> GetOrdersByStatus(Guid memberId, int status);
        IEnumerable<Order> GetAllOrders(Guid memberId);

    }
}
