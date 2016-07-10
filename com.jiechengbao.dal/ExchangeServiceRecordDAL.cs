using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class ExchangeServiceRecordDAL : DataBaseDAL<ExchangeServiceRecord>, IExchangeServiceRecordDAL
    {
        public IEnumerable<ExchangeServiceRecord> SelectByMemberId(Guid memberId)
        {
            try
            {
                return db.Set<ExchangeServiceRecord>().Where(n => n.MemberId == memberId && n.IsUse == false && n.IsDeleted == false);
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
