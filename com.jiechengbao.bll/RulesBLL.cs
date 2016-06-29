using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.bll
{
    public class RulesBLL:IRulesBLL
    {
        private IRulesDAL _rulesDAL;
        public RulesBLL(IRulesDAL rulesDAL)
        {
            _rulesDAL = rulesDAL;
        }

        /// <summary>
        /// 根据VIP等级找到折扣值
        /// </summary>
        /// <param name="VIP"></param>
        /// <returns></returns>
        public double GetDiscountByVIP(int VIP)
        {
            Rules rules = _rulesDAL.SelectByVIP(VIP);
            if (rules == null)
            {
                return 1;
            }
            return rules.Discount;
        }
    }
}
