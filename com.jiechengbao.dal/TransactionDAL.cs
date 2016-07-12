using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class TransactionDAL : DataBaseDAL<Transaction>, ITransactionDAL
    {
        public IEnumerable<Transaction> SelectByMemberIdwithCount(Guid memberId, int count)
        {
            try
            {
                return db.Set<Transaction>().Where(n => n.MemberId == memberId).OrderByDescending(n => n.CreatedTime).Take(count);
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
