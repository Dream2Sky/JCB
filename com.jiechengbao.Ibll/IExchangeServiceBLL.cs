using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Ibll
{
    public interface IExchangeServiceBLL
    {
        IEnumerable<ExchangeService> GetAllNoDeletedExchangeServiceList();
        ExchangeService GetNoDeletedExchangeServiceByCode(string code);
        ExchangeService GetNoDeletedExchangeServiceById(Guid Id);
        IEnumerable<ExchangeService> GetNoDeletedExchangeServiceByAnyCondition(string condition);
        IEnumerable<ExchangeService> GetExchangeServiceByName(string name);
        bool Add(ExchangeService es);
        bool Update(ExchangeService es);
    }
}
