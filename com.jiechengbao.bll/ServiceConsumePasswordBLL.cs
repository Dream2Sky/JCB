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
    public class ServiceConsumePasswordBLL : IServiceConsumePasswordBLL
    {
        private IServiceConsumePasswordDAL _serviceConsumePasswordDAL;
        public ServiceConsumePasswordBLL(IServiceConsumePasswordDAL serviceConsumePasswordDAL)
        {
            _serviceConsumePasswordDAL = serviceConsumePasswordDAL;
        } 

        public ServiceConsumePassword GetServicePassword()
        {
            return _serviceConsumePasswordDAL.SelectAll().FirstOrDefault();
        }
        public bool Update(ServiceConsumePassword scp)
        {
            return _serviceConsumePasswordDAL.Update(scp);
        }
    }
}
