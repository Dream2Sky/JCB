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
    public class ServiceBLL : IServiceBLL
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

        public IEnumerable<MyService> GetMyServiceByMemberId(Guid memberId)
        {
            // 找到当前用户的所有可用的服务列表
            try
            {
                return _serviceDAL.SelectByMemberId(memberId).Where(n => n.CurrentCount > 0);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                return null;
            }
            
        }



        public MyService GetMyServiceByServiceId(Guid serviceId)
        {
            return _serviceDAL.SelectById(serviceId);
        }

        public bool Update(MyService ms)
        {
            return _serviceDAL.Update(ms);
        }
    }
}
