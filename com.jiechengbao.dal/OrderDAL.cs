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
        public IEnumerable<Order> SelectAllByMemberId(Guid memberId)
        {
            try
            {
                return db.Set<Order>().Where(n => n.MemberId == memberId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        public Order SelectByOrderNo(string orderNo)
        {
            try
            {
                return db.Set<Order>().SingleOrDefault(n => n.OrderNo == orderNo);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        public IEnumerable<Order> SelectByStatus(int status)
        {
            try
            {
                return db.Set<Order>().Where(n => n.Status == status);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        public IEnumerable<Order> SelectByStatus(int status, Guid memberId)
        {
            try
            {
                return db.Set<Order>().Where(n => n.Status == status).Where(n => n.MemberId == memberId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

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
