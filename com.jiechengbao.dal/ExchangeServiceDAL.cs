using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class ExchangeServiceDAL : DataBaseDAL<ExchangeService>, IExchangeServiceDAL
    {
        public IEnumerable<ExchangeService> SelectByAnyCondition(string condition)
        {
            try
            {
                return db.Set<ExchangeService>().Where(n => n.Name.Contains(condition));
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        public ExchangeService selectByCode(string code)
        {
            try
            {
                return db.Set<ExchangeService>().SingleOrDefault(n => n.Code == code && n.IsDeleted == false);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        public IEnumerable<ExchangeService> SelectByName(string name)
        {
            try
            {
                return db.Set<ExchangeService>().Where(n => n.Name.Contains(name));
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
