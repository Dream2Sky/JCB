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
    public class ServiceConsumeRecordBLL:IServiceConsumeRecordBLL
    {
        private IServiceConsumeRecordDAL _serviceConsumeRecordDAL;
        public ServiceConsumeRecordBLL(IServiceConsumeRecordDAL serviceConsumeRecordDAL)
        {
            _serviceConsumeRecordDAL = serviceConsumeRecordDAL;
        }

        public bool Add(ServiceConsumeRecord scr)
        {
            return _serviceConsumeRecordDAL.Insert(scr);
        }
    }
}
