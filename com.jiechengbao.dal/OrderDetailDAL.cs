using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class OrderDetailDAL : DataBaseDAL<OrderDetail>, IOrderDetailDAL
    {
        public IEnumerable<OrderDetail> SelectByOrderNo(string orderNO)
        {
            try
            {
                return db.Set<OrderDetail>().Where(n => n.OrderNo == orderNO);
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
