using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Idal
{
    public interface IOrderDAL:IDataBaseDAL<Order>
    {
        IEnumerable<Order> SelectOrderByDate(DateTime date);
        IEnumerable<Order> SelectByStatus(int status);
        Order SelectByOrderNo(string orderNo);
        IEnumerable<Order> SelectByStatus(int status, Guid memberId);
        IEnumerable<Order> SelectAllByMemberId(Guid memberId);
    }
}
