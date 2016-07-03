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

        public bool Add(Rules rules)
        {
            return _rulesDAL.Insert(rules);
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="rulesList"></param>
        /// <returns></returns>
        public bool Add(List<Rules> rulesList)
        {
            return _rulesDAL.Insert(rulesList);
        }

        public bool Clear()
        {
            return _rulesDAL.Clear();
        }

        public List<Rules> GetAllRules()
        {
            return _rulesDAL.SelectAll().ToList();
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

        public bool UpGradeVIP(double totalCredit, int currentVIP, out int TargetVIP)
        {
            Rules rules = _rulesDAL.SelectByTotalCreditWithOrderDesc(totalCredit);

            if (rules.VIP > currentVIP)
            {
                TargetVIP = rules.VIP;
                return true;
            }
            else
            {
                TargetVIP = 0;
                return false;
            }
        }
    }
}
