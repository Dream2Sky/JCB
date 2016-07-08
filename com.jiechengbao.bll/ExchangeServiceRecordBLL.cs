using com.jiechengbao.Ibll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.bll
{
    public class ExchangeServiceRecordBLL:IExchangeServiceRecordBLL
    {
        private IExchangeServiceRecordBLL _exchangeServiceRecordBLL;
        public ExchangeServiceRecordBLL(IExchangeServiceRecordBLL exchangeServiceRecordBLL)
        {
            _exchangeServiceRecordBLL = exchangeServiceRecordBLL;
        }
    }
}
