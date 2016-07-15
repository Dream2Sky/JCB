using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Ibll
{
    public interface IOrderStatusBLL
    {
        OrderStatus GetOrderStatusByOrderId(Guid orderId);
        bool Update(OrderStatus os);
        bool Add(OrderStatus os);
    }
}
