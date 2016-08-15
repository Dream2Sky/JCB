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
    public class FreeCouponBLL:IFreeCouponBLL
    {
        private IFreeCouponDAL _freeCouponDAL;
        public FreeCouponBLL(IFreeCouponDAL freeCouponDAL)
        {
            _freeCouponDAL = freeCouponDAL;
        }

        public IEnumerable<FreeCoupon> GetAllNotDeletedCoupon()
        {
            return _freeCouponDAL.SelectAllNotDeletedCoupon();
        }

        public FreeCoupon GetFreeCouponByCode(string code)
        {
            return _freeCouponDAL.SelectByCode(code);
        }

        public FreeCoupon GetFreeCouponById(Guid freeCouponId)
        {
            return _freeCouponDAL.SelectById(freeCouponId);
        }
    }
}
