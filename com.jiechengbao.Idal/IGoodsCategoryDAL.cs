using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Idal
{
    public interface IGoodsCategoryDAL:IDataBaseDAL<GoodsCategory>
    {
        IEnumerable<GoodsCategory> SelectGoodsCategoryListByGoodsId(Guid goodsId);
        bool DeleteByGoodsId(Guid goodsId);
    }
}
