using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class MyAppointmentItemDAL : DataBaseDAL<MyAppointmentItem>, IMyAppointmentItemDAL
    {
        /// <summary>
        /// 根据我的预约单的id 找到所预约的服务项
        /// </summary>
        /// <param name="MyAppointmentId"></param>
        /// <returns></returns>
        public IEnumerable<MyAppointmentItem> SelectByMyAppointmentId(Guid MyAppointmentId)
        {
            try
            {
                return db.Set<MyAppointmentItem>().Where(n => n.MyAppointmentId == MyAppointmentId);
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
