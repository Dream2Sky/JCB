using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Ibll
{
    public interface IRechargeBLL
    {
        IEnumerable<Recharge> GetRechargeListByMemberId(DateTime startTime, DateTime endTime, Guid MemberId);
        bool Add(Recharge recharge);
    }
}
