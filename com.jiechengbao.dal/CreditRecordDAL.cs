using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class CreditRecordDAL : DataBaseDAL<CreditRecord>, ICreditRecordDAL
    {
        public IEnumerable<CreditRecord> SelectByMemberId(Guid memberId)
        {
            try
            {
                DateTime now = DateTime.Now.AddMonths(-1);
                return db.Set<CreditRecord>().Where(n => n.MemberId == memberId).Where(n => n.CreatedTime >= now).OrderByDescending(n => n.CreatedTime);
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
