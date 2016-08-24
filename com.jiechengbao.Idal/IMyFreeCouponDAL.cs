﻿using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Idal
{
    public interface IMyFreeCouponDAL:IDataBaseDAL<MyFreeCoupon>
    {
        IEnumerable<MyFreeCoupon> SelectAllNotDeletedMyFreeCouponsByMemberId(Guid memberId);
        MyFreeCoupon SelectByMemberId(Guid memberId);
    }
}
