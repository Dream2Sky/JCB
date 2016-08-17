using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Ibll
{
    public interface IFreeCouponBLL
    {
        IEnumerable<FreeCoupon> GetAllNotDeletedCoupon();
        FreeCoupon GetFreeCouponByCode(string code);
        FreeCoupon GetFreeCouponById(Guid freeCouponId);
        bool Add(FreeCoupon fc);
        bool IsExist(FreeCoupon fc);
        bool Update(FreeCoupon fc);
    }
}
