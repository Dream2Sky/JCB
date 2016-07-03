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
    public class CreditRecordBLL:ICreditRecordBLL
    {
        private ICreditRecordDAL _creditRecordDAL;
        public CreditRecordBLL(ICreditRecordDAL creditRecordDAL)
        {
            _creditRecordDAL = creditRecordDAL;
        }

        public bool Add(CreditRecord cr)
        {
            return _creditRecordDAL.Insert(cr);
        }
    }
}
