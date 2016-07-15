using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class OrderStatusDAL : DataBaseDAL<OrderStatus>, IOrderStatusDAL
    {
        public OrderStatus SelectByOrderId(Guid orderId)
        {
            try
            {
                return db.Set<OrderStatus>().SingleOrDefault(n => n.OrderId == orderId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }
    }
}
