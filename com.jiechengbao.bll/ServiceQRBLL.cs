using com.jiechengbao.Ibll;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.bll
{
    public class ServiceQRBLL:IServiceQRBLL
    {
        private IServiceQRDAL _serviceQRDAL;
        public ServiceQRBLL(IServiceQRDAL serviceQRDAL)
        {
            _serviceQRDAL = serviceQRDAL;
        }
    }
}
