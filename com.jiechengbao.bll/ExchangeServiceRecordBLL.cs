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
    public class ExchangeServiceRecordBLL:IExchangeServiceRecordBLL
    {
        private IExchangeServiceRecordDAL _exchangeServiceRecordDAL;
        public ExchangeServiceRecordBLL(IExchangeServiceRecordDAL exchangeServiceRecordDAL)
        {
            _exchangeServiceRecordDAL = exchangeServiceRecordDAL;
        }

        public bool Add(ExchangeServiceRecord esr)
        {
            return _exchangeServiceRecordDAL.Insert(esr);
        }

        public ExchangeServiceRecord GetESRById(Guid Id)
        {
            return _exchangeServiceRecordDAL.SelectById(Id);
        }

        public IEnumerable<ExchangeServiceRecord> GetMyESR(Guid MemberId)
        {
            return _exchangeServiceRecordDAL.SelectByMemberId(MemberId);
        }

        public bool Update(ExchangeServiceRecord esr)
        {
            return _exchangeServiceRecordDAL.Update(esr);
        }
    }
}
