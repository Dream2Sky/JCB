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
        public bool Delete(List<OrderDetail> odList)
        {
            try
            {
                foreach (var item in odList)
                {
                    db.Set<OrderDetail>().Attach(item);
                    db.Entry<OrderDetail>(item).State = System.Data.Entity.EntityState.Deleted;
                    db.Set<OrderDetail>().Remove(item);     
                }
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                return false;
            }
        }

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
