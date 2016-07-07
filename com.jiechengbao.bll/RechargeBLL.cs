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
    public class RechargeBLL:IRechargeBLL
    {
        private IRechargeDAL _rechargeDAL;
        public RechargeBLL(IRechargeDAL rechargeDAL)
        {
            _rechargeDAL = rechargeDAL;
        }

        public bool Add(Recharge recharge)
        {
            return _rechargeDAL.Insert(recharge);
        }

        public IEnumerable<Recharge> GetRechargeListByMemberId(DateTime startTime, DateTime endTime, Guid memberId)
        {
            return _rechargeDAL.SelectRechargeListByMemberId(startTime, endTime, memberId);
        }

        /// <summary>
        /// 返回最近10条充值记录
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public IEnumerable<Recharge> GetRechargeListTop10ByMemberId(Guid memberId)
        {
            return _rechargeDAL.SelectRechargeListByMemberIdTakeTopCount(memberId, 10);
        }
    }
}
