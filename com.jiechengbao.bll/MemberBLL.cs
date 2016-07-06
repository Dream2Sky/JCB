using com.jiechengbao.Ibll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.jiechengbao.entity;
using com.jiechengbao.Idal;

namespace com.jiechengbao.bll
{
    public class MemberBLL : IMemberBLL
    {
        private IMemberDAL _memberDAL;
        public MemberBLL(IMemberDAL memberDAL)
        {
            _memberDAL = memberDAL;
        }

        public bool Add(Member member)
        {
            return _memberDAL.Insert(member);
        }

        /// <summary>
        /// 获取未标记为删除的用户的集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Member> GetAllNoDeletedMembers()
        {
            try
            {
                return _memberDAL.SelectAll().Where(n => n.IsDeleted == false);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);

                return null;
            }
            
        }

        /// <summary>
        /// 获得所有未标记为删除的用户的数量
        /// </summary>
        /// <returns></returns>
        public int GetAllNoDeletedMembersCount()
        {
            return _memberDAL.SelectNoDeletedMembersCount();
        }

        /// <summary>
        /// 根据MemberID 获取一个Member
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Member GetMemberById(Guid id)
        {
            return _memberDAL.SelectById(id);
        }

        /// <summary>
        /// 根据微信openid 获取Member对象
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public Member GetMemberByOpenId(string openId)
        {
            return _memberDAL.SelectByWxOpenId(openId);
        }

        /// <summary>
        /// 获得指定数量的用户列表  已倒序排序
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public IEnumerable<Member> GetMemberswithSpecifiedCount(int count)
        {
            return _memberDAL.SelectNoDeletedMemberswithSpecifiedCount(count);
        }

        /// <summary>
        /// 获得昨天的新用户列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Member> GetNewMembersAtYesterDay()
        {
            return _memberDAL.SelectNoDeletedMembersByDate(DateTime.Now.AddDays(-1).Date);
        }

        /// <summary>
        /// 判断指定的openid用户是否已经注册
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public bool IsExist(string openId)
        {
            if (GetMemberByOpenId(openId) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        ///  修改对象
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public bool Update(Member member)
        {
            return _memberDAL.Update(member);
        }
    }
}
