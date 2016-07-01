using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Idal
{
    public interface IRechargeDAL:IDataBaseDAL<Recharge>
    {
        IEnumerable<Recharge> SelectRechargeListByMemberId(DateTime startTime, DateTime endTime, Guid memberId);
    }
}
