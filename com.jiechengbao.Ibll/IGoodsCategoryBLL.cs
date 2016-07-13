using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Ibll
{
    public interface IGoodsCategoryBLL
    {
        bool Add(GoodsCategory gc);
        IEnumerable<GoodsCategory> GetGoodsCategoryListByGoodsId(Guid Id);
        bool RemoveGoodsCategoryByGoodsId(Guid goodsId);
        IEnumerable<GoodsCategory> GetGoodsCategoryListByCategoryId(Guid categoryId);

        bool Update(GoodsCategory gc);
    }
}
