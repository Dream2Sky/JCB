using com.jiechengbao.Ibll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.jiechengbao.entity;
using com.jiechengbao.Idal;

namespace com.jiechengbao.bll
{
    public class GoodsImagesBLL : IGoodsImagesBLL
    {
        private IGoodsImagesDAL _goodsImagesDAL;
        public GoodsImagesBLL(IGoodsImagesDAL goodsImagesDAL)
        {
            _goodsImagesDAL = goodsImagesDAL;
        }
        public bool Add(GoodsImage gi)
        {
            return _goodsImagesDAL.Insert(gi);
        }
    }
}
