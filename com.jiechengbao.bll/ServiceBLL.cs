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
    public class ServiceBLL:IServiceBLL
    {
        private IServiceDAL _serviceDAL;
        public ServiceBLL(IServiceDAL serviceDAL)
        {
            _serviceDAL = serviceDAL;
        }

        public bool Add(MyService ms)
        {
            return _serviceDAL.Insert(ms);
        }
    }
}
