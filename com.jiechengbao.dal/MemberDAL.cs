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
        public Member SelectByNickNameandPhone(string condition)
        {
            try
            {
                return db.Set<Member>().Where(n => n.NickeName == condition || n.Phone == condition).SingleOrDefault();
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        public Member SelectByWxOpenId(string openId)
        {
            try
            {
                return db.Set<Member>().SingleOrDefault(n => n.OpenId == openId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

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

        public IEnumerable<Member> SelectNoDeletedMemberswithSpecifiedCount(int count)
        {
            try
            {
                return db.Set<Member>().OrderByDescending(n => n.CreatedTime).Take(count);
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
