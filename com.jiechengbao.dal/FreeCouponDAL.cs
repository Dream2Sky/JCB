using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class FreeCouponDAL : DataBaseDAL<FreeCoupon>, IFreeCouponDAL
    {
        /// <summary>
        /// select all not deleted coupons orderby createdTime desc;
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FreeCoupon> SelectAllNotDeletedCoupon()
        {
            try
            {
                return db.Set<FreeCoupon>().Where(n => n.IsDeleted == false).OrderByDescending(n => n.CreatedTime);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        public FreeCoupon SelectByCode(string code)
        {
            try
            {
                return db.Set<FreeCoupon>().SingleOrDefault(n => n.CouponCode == code);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        public bool IsExist(string name)
        {
            try
            {
                bool res = false;
                if (db.Set<FreeCoupon>().SingleOrDefault(n => n.CouponName == name && n.IsDeleted == false) != null)
                {
                    res = true;
                }
                return res;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }
    }
}
