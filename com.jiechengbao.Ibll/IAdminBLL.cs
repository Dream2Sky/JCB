using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Ibll
{
    public interface IAdminBLL
    {
        bool Login(string account, string password);
        bool Add(Admin admin);
        Admin GetAdminByAccount(string account);
    }
}
