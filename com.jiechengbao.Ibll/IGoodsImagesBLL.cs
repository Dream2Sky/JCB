using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Ibll
{
    public interface IGoodsImagesBLL
    {
        bool Add(GoodsImage gi);
        GoodsImage GetPictureByGoodsId(Guid goodsId);
        bool Update(GoodsImage gi);
    }
}
