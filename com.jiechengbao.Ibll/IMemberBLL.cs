using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Ibll
{
    public interface IMemberBLL
    {
        IEnumerable<Member> GetAllNoDeletedMembers();
        int GetAllNoDeletedMembersCount();
        IEnumerable<Member> GetNewMembersAtYesterDay();
        Member GetMemberById(Guid id);
        Member GetMemberByOpenId(string openId);
        bool Update(Member member);
        IEnumerable<Member> GetMemberswithSpecifiedCount(int count);
        bool IsExist(string openId);
        bool Add(Member member);
        IEnumerable<Member> GetMembersByNickNameAndPhone(string condition);

        IEnumerable<Member> GetNewMembersAWeek();
    }
}
