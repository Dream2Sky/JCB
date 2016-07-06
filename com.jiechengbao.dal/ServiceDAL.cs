using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class ServiceDAL : DataBaseDAL<MyService>, IServiceDAL
    {
        public IEnumerable<MyService> SelectByMemberId(Guid memberId)
        {
            try
            {
                IEnumerable<MyService> msList = db.Set<MyService>().Where(n => n.MemberId == memberId);
                if (msList == null)
                {
                    LogHelper.Log.Write("DAL: msList is null");
                }
                return msList;
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
