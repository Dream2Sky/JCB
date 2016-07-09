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

        public IEnumerable<ExchangeService> GetAllNoDeletedExchangeServiceList()
        {
            return _exchangeServiceDAL.SelectAll().Where(n => n.IsDeleted == false);
        }

        public ExchangeService GetNoDeletedExchangeServiceByCode(string code)
        {
            return _exchangeServiceDAL.selectByCode(code);
        }
    }
}
