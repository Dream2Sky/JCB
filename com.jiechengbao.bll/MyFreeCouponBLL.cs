using com.jiechengbao.Ibll;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.jiechengbao.entity;

namespace com.jiechengbao.bll
{
    public class MyFreeCouponBLL:IMyFreeCouponBLL
    {
        private IMyFreeCouponDAL _myFreeCouponDAL;
        public MyFreeCouponBLL(IMyFreeCouponDAL myFreeCouponDAL)
        {
            _myFreeCouponDAL = myFreeCouponDAL;
        }

        public bool Add(MyFreeCoupon myFreeCoupon)
        {
            return _myFreeCouponDAL.Insert(myFreeCoupon);
        }

        public MyFreeCoupon GetMyFreeCouponById(Guid myFreeCouponId)
        {
            return _myFreeCouponDAL.SelectById(myFreeCouponId);
        }

        public IEnumerable<MyFreeCoupon> GetMyFreeCouponList(Guid memberId)
        {
            return _myFreeCouponDAL.SelectAllNotDeletedMyFreeCouponsByMemberId(memberId);
        }

        public bool Update(MyFreeCoupon myFreeCoupon)
        {
            return _myFreeCouponDAL.Update(myFreeCoupon);
        }
    }
}
