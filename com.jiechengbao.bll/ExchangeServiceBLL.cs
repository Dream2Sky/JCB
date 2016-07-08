using com.jiechengbao.Ibll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.bll
{
    public class ExchangeServiceBLL:IExchangeServiceBLL
    {
        private IExchangeServiceBLL _exchangeServiceBLL;
        public ExchangeServiceBLL(IExchangeServiceBLL exchangeServiceBLL)
        {
            _exchangeServiceBLL = exchangeServiceBLL;
        }
    }
}
