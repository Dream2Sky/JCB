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
    public class ExchangeServiceQRBLL:IExchangeServiceQRBLL
    {
        private IExchangeServiceQRDAL _exchangeServiceQRDAL;
        public ExchangeServiceQRBLL(IExchangeServiceQRDAL exchangeServiceQRDAL)
        {
            _exchangeServiceQRDAL = exchangeServiceQRDAL;
        }

        public bool Add(ExchangeServiceQR qr)
        {
            return _exchangeServiceQRDAL.Insert(qr);
        }

        public ExchangeServiceQR GetExchangeServiceQRById(Guid exchangeServiceRecordId)
        {
            return _exchangeServiceQRDAL.SelectByExchangeServiceId(exchangeServiceRecordId);
        }
    }
}
