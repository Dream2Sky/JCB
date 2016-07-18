using com.jiechengbao.Ibll;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.bll
{
    public class TransactionDetailBLL:ITransactionDetailBLL
    {
        private ITransactionDetailDAL _transactionDetailDAL;
        public TransactionDetailBLL(ITransactionDetailDAL transactionDetailDAL)
        {
            _transactionDetailDAL = transactionDetailDAL;
        }

    }
}
