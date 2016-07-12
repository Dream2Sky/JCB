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
    public class TransactionBLL : ITransactionBLL
    {
        private ITransactionDAL _transactionDAL;
        public TransactionBLL(ITransactionDAL transactionDAL)
        {
            _transactionDAL = transactionDAL;
        }

        public bool Add(Transaction transaction)
        {
            return _transactionDAL.Insert(transaction);
        }

        public IEnumerable<Transaction> GetTransactionByMemberIdwithCount(Guid memberId, int count)
        {
            return _transactionDAL.SelectByMemberIdwithCount(memberId, count);
        }

        public bool Remove(Transaction transaction)
        {
            return _transactionDAL.Delete(transaction);
        }
    }
}
