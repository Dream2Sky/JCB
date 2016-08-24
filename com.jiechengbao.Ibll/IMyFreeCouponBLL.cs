using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Ibll
{
    public interface IMyFreeCouponBLL
    {
        bool Add(MyFreeCoupon myFreeCoupon);
        bool Update(MyFreeCoupon myFreeCoupon);
        IEnumerable<MyFreeCoupon> GetMyFreeCouponList(Guid memberId);
        MyFreeCoupon GetMyFreeCouponById(Guid myFreeCouponId);
        bool IsAlreadyPicked(Guid memberId);
    }
}
