using com.jiechengbao.Ibll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.jiechengbao.entity;
using com.jiechengbao.Idal;

namespace com.jiechengbao.bll
{
    public class OrderStatusBLL : IOrderStatusBLL
    {
        private IOrderStatusDAL _orderStatusDAL;
        public OrderStatusBLL(IOrderStatusDAL orderStatusDAL)
        {
            _orderStatusDAL = orderStatusDAL;
        }

        public bool Add(OrderStatus os)
        {
            return _orderStatusDAL.Insert(os);
        }

        public OrderStatus GetOrderStatusByOrderId(Guid orderId)
        {
            return _orderStatusDAL.SelectByOrderId(orderId);
        }

        public bool Update(OrderStatus os)
        {
            return _orderStatusDAL.Update(os);
        }
    }
}
