using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Idal
{
    public interface IFreeCouponDAL:IDataBaseDAL<FreeCoupon>
    {
        IEnumerable<FreeCoupon> SelectAllNotDeletedCoupon();
        FreeCoupon SelectByCode(string code);
        bool IsExist(string name);

    }
}
