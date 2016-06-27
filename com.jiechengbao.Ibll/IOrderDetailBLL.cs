using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Ibll
{
    public interface IOrderDetailBLL
    {
        bool Add(OrderDetail od);
        IEnumerable<OrderDetail> GetOrderDetailByOrderNo(string orderNo);
        bool Remove(OrderDetail od);
        bool Remove(List<OrderDetail> odList);
    }
}
