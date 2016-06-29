using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Ibll
{
    public interface IReCommendBLL
    {
        IEnumerable<ReCommend> GetAllReCommendListwithSortByTime();
        bool Add(ReCommend commend);
        bool IsRecommend(Guid goodsId);
        bool Remove(Guid goodsId);
    }
}
