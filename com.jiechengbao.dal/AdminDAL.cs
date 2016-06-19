using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class AdminDAL : DataBaseDAL<Admin>, IAdminDAL
    {
        /// <summary>
        /// 通过账号查询 管理员对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Admin SelectByName(string name)
        {
            try
            {
                return  db.Set<Admin>().SingleOrDefault(n => n.Account == name);
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
