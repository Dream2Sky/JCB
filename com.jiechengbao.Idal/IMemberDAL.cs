using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Idal
{
    public interface IMemberDAL:IDataBaseDAL<Member>
    {
        int SelectNoDeletedMembersCount();
        IEnumerable<Member> SelectNoDeletedMembersByDate(DateTime date);
    }
}
