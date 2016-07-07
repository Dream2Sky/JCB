using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class RechargeDAL : DataBaseDAL<Recharge>, IRechargeDAL
    {
        public IEnumerable<Recharge> SelectRechargeListByMemberId(DateTime startTime, DateTime endTime, Guid memberId)
        {
            try
            {
                return db.Set<Recharge>().Where(n => n.CreatedTime >= startTime).Where(n => n.CreatedTime <= endTime).Where(n => n.MemberId == memberId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        public IEnumerable<Recharge> SelectRechargeListByMemberIdTakeTopCount(Guid memberId, int count)
        {
            try
            {
                return db.Set<Recharge>().Take(count).OrderByDescending(n => n.CreatedTime);
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
