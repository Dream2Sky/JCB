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
    public class OrderDetailBLL:IOrderDetailBLL
    {
        private IOrderDetailDAL _orderDetailDAL;
        public OrderDetailBLL(IOrderDetailDAL orderDetailDAL)
        {
            _orderDetailDAL = orderDetailDAL;
        }

        public bool Add(OrderDetail od)
        {
            return _orderDetailDAL.Insert(od);
        }

        public IEnumerable<OrderDetail> GetOrderDetailByOrderNo(string orderNo)
        {
            return _orderDetailDAL.SelectByOrderNo(orderNo);
        }
    }
}
