using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class ServiceQRDAL : DataBaseDAL<ServiceQR>, IServiceQRDAL
    {
        public ServiceQR SelectByServcieId(Guid serviceId)
        {
            try
            {
                return db.Set<ServiceQR>().Where(n => n.ServcieId == serviceId).SingleOrDefault();
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
