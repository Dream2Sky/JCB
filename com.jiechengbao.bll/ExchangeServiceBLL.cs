using com.jiechengbao.Ibll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.jiechengbao.entity;
using com.jiechengbao.Idal;

namespace com.jiechengbao.bll
{
    public class ExchangeServiceBLL:IExchangeServiceBLL
    {
        private IExchangeServiceDAL _exchangeServiceDAL;
        public ExchangeServiceBLL(IExchangeServiceDAL exchangeServiceDAL)
        {
            _exchangeServiceDAL = exchangeServiceDAL;
        }

        public bool Add(ExchangeService es)
        {
            return _exchangeServiceDAL.Insert(es);
        }

        public IEnumerable<ExchangeService> GetAllNoDeletedExchangeServiceList()
        {
            return _exchangeServiceDAL.SelectAll().Where(n => n.IsDeleted == false).OrderByDescending(n=>n.CreatedTime);
        }

        public IEnumerable<ExchangeService> GetExchangeServiceByName(string name)
        {
            return _exchangeServiceDAL.SelectByName(name);
        }

        public IEnumerable<ExchangeService> GetNoDeletedExchangeServiceByAnyCondition(string condition)
        {
            return _exchangeServiceDAL.SelectByAnyCondition(condition);
        }

        public ExchangeService GetNoDeletedExchangeServiceByCode(string code)
        {
            return _exchangeServiceDAL.selectByCode(code);
        }

        public ExchangeService GetNoDeletedExchangeServiceById(Guid Id)
        {
            return _exchangeServiceDAL.SelectById(Id);
        }

        public bool Update(ExchangeService es)
        {
            return _exchangeServiceDAL.Update(es);
        }
    }
}
