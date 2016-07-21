using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class MyAppointmentDAL : DataBaseDAL<MyAppointment>, IMyAppointmentDAL
    {
        /// <summary>
        /// 找到指定会员预约单列表
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public IEnumerable<MyAppointment> SelectByMemberIdAndPay(Guid memberId,bool isPay)
        {
            try
            {
                return db.Set<MyAppointment>().Where(n => n.MemberId == memberId && n.IsPay == isPay);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// 查出已付款或未付款的预约单
        /// </summary>
        /// <param name="isPay"></param>
        /// <returns></returns>
        public IEnumerable<MyAppointment> SelectByPay(bool isPay)
        {
            try
            {
                return db.Set<MyAppointment>().Where(n => n.IsDeleted == false && n.IsPay == isPay);
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
