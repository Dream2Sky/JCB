using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class MemberDAL : DataBaseDAL<Member>, IMemberDAL
    {
        public IEnumerable<Member> SelectNoDeletedMembersByDate(DateTime date)
        {
            try
            {
                return db.Set<Member>().Where(n => n.CreatedTime == date);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        public int SelectNoDeletedMembersCount()
        {
            try
            {
                return db.Set<Member>().Where(n => n.IsDeleted == false).Count();
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);

                return 0;
            }
        }
    }
}
