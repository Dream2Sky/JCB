using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class AppointmentServiceDAL : DataBaseDAL<AppointmentService>, IAppointmentServiceDAL
    {
        /// <summary>
        /// 判断该服务项是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistByName(string name)
        {
            try
            {
                AppointmentService appoint = db.Set<AppointmentService>().Where(n => n.Name == name).SingleOrDefault();
                if (appoint == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                    
                throw;
            }
        }

        /// <summary>
        /// 找到所有的未删除的预约服务
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AppointmentService> SelectAllButNotDeleted()
        {
            try
            {
                return db.Set<AppointmentService>().Where(n => n.IsDeleted == false);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// 按 编号查询
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public AppointmentService SelectByCode(string code)
        {
            try
            {
                return db.Set<AppointmentService>().SingleOrDefault(n => n.Code == code);
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
