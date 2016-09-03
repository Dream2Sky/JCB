﻿using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class MyFreeCouponDAL : DataBaseDAL<MyFreeCoupon>, IMyFreeCouponDAL
    {
        public IEnumerable<MyFreeCoupon> SelectAllNotDeletedMyFreeCouponsByMemberId(Guid memberId)
        {
            try
            {
                return db.Set<MyFreeCoupon>().Where(n => n.MemberId == memberId && n.IsDeleted == false);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        ///  找出一个指定 Id 的我的优惠券
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public MyFreeCoupon SelectByMemberId(Guid memberId)
        {
            try
            {
                return db.Set<MyFreeCoupon>().Where(n => n.MemberId == memberId).FirstOrDefault();
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
