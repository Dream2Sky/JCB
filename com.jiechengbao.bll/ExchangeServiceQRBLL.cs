using com.jiechengbao.Ibll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.jiechengbao.entity;

namespace com.jiechengbao.bll
{
    public class ExchangeServiceQRBLL:IExchangeServiceQRBLL
    {
        private IExchangeServiceQRBLL _exchangeServiceQRBLL;
        public ExchangeServiceQRBLL(IExchangeServiceQRBLL exchangeServiceQRBLL)
        {
            _exchangeServiceQRBLL = exchangeServiceQRBLL;
        }

        public bool Add(ExchangeServiceQR qr)
        {
            return _exchangeServiceQRBLL.Add(qr);
        }

        public ExchangeServiceQR GetExchangeServiceQRById(Guid exchangeServiceId)
        {
            return _exchangeServiceQRBLL.GetExchangeServiceQRById(exchangeServiceId);
        }
    }
}
