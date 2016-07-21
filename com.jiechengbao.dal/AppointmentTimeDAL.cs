using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class AppointmentTimeDAL : DataBaseDAL<AppointmentTime>, IAppointmentTimeDAL
    {
        /// <summary>
        /// 获取最新的时间段配置
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AppointmentTime> SelectAppointmentTimeLastDay()
        {
            try
            {
                return db.Set<AppointmentTime>().Where(n => n.IsDeleted == false);
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
