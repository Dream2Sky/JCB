using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class OrderDAL : DataBaseDAL<Order>, IOrderDAL
    {
        public IEnumerable<Order> SelectOrderByDate(DateTime date)
        {
            try
            {
                return db.Set<Order>().Where(n => n.CreatedTime == date);
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
