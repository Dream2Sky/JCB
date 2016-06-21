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
    public class GoodsBLL : IGoodsBLL
    {
        private IGoodsDAL _goodsDAL;
        public GoodsBLL(IGoodsDAL goodsDAL)
        {
            _goodsDAL = goodsDAL;
        }

        public bool Add(Goods goods)
        {
            return _goodsDAL.Insert(goods);
        }

        /// <summary>
        /// 获取未标记为删除的商品
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Goods> GetAllNoDeteledGoods()
        {
            return _goodsDAL.SelectAll().Where(n => n.IsDeleted == false);
        }
    }
}
