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
    public class GoodsCategoryBLL : IGoodsCategoryBLL
    {
        private IGoodsCategoryDAL _goodsCategoryDAL;
        public GoodsCategoryBLL(IGoodsCategoryDAL goodsCategoryDAL)
        {
            _goodsCategoryDAL = goodsCategoryDAL;
        }
        public bool Add(GoodsCategory gc)
        {
            return _goodsCategoryDAL.Insert(gc);
        }
    }
}
