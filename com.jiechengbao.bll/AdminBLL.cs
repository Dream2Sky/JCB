using com.jiechengbao.Ibll;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.jiechengbao.entity;
using com.jiechengbao.common;

namespace com.jiechengbao.bll
{
    public class AdminBLL:IAdminBLL
    {
        private IAdminDAL _adminDAL;
        public AdminBLL(IAdminDAL adminDAL)
        {
            _adminDAL = adminDAL;
        }

        public bool Add(Admin admin)
        {
            return _adminDAL.Insert(admin);
        }

        public Admin GetAdminByAccount(string account)
        {
            return _adminDAL.SelectByName(account);
        }

        public bool Login(string account, string password)
        {
            try
            {
                Admin admin = _adminDAL.SelectByName(account);
                if (admin == null)
                {
                    return false;
                }
                else if(admin.Password == EncryptManager.SHA1(password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
