using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class ExchangeServiceQRDAL : DataBaseDAL<ExchangeServiceQR>, IExchangeServiceQRDAL
    {
        public ExchangeServiceQR SelectByExchangeServiceId(Guid exchangeServiceRecordId)
        {
            try
            {
                return db.Set<ExchangeServiceQR>().Where(n => n.ExchangeServiceId == exchangeServiceRecordId).SingleOrDefault();
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
