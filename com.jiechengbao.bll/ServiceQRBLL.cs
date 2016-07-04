using com.jiechengbao.Ibll;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.jiechengbao.entity;

namespace com.jiechengbao.bll
{
    public class ServiceQRBLL:IServiceQRBLL
    {
        private IServiceQRDAL _serviceQRDAL;
        public ServiceQRBLL(IServiceQRDAL serviceQRDAL)
        {
            _serviceQRDAL = serviceQRDAL;
        }

        public bool Add(ServiceQR qr)
        {
            return _serviceQRDAL.Insert(qr);
        }

        public ServiceQR GetServiceQRByServcieId(Guid serviceId)
        {
            return _serviceQRDAL.SelectByServcieId(serviceId);
        }
    }
}
