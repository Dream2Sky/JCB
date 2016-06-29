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
