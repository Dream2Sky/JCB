using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class RulesDAL : DataBaseDAL<Rules>, IRulesDAL
    {
        /// <summary>
        ///  批量添加
        /// </summary>
        /// <param name="rulesList"></param>
        /// <returns></returns>
        public bool Insert(List<Rules> rulesList)
        {
            try
            {
                foreach (var item in rulesList)
                {
                    db.Ruleses.Attach(item);
                    db.Entry(item).State = System.Data.Entity.EntityState.Added;
                }
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        public Rules SelectByTotalCreditWithOrderDesc(double totalCredit)
        {
            try
            {
                // 找到比传递进来的总积分小的最大的vip规则
                Rules rule = db.Set<Rules>().Where(n => n.TotalCredit <= totalCredit).OrderByDescending(n => n.TotalCredit).FirstOrDefault();

                return rule;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        public Rules SelectByVIP(int VIP)
        {
            try
            {
                return db.Set<Rules>().SingleOrDefault(n => n.VIP == VIP);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }
    }
}
