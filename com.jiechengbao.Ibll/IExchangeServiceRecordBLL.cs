using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Ibll
{
    public interface IExchangeServiceRecordBLL
    {
        bool Add(ExchangeServiceRecord esr);
        ExchangeServiceRecord GetESRById(Guid Id);
        bool Update(ExchangeServiceRecord esr);
        IEnumerable<ExchangeServiceRecord> GetMyESR(Guid MemberId);
    }
}
