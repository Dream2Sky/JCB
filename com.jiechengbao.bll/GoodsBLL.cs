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

        /// <summary>
        /// 添加新商品 
        /// 在添加新商品之前 要判断是否存在同名商品
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public bool Add(Goods goods)
        {
            if (_goodsDAL.SelectByName(goods.Name) == null)
            {
                return _goodsDAL.Insert(goods);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取未标记为删除的商品
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Goods> GetAllNoDeteledGoods()
        {
            return _goodsDAL.SelectAll().Where(n => n.IsDeleted == false);
        }

        /// <summary>
        /// 根据 商品编号 获取商品对象
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Goods GetGoodsByCode(string code)
        {
            return _goodsDAL.SelectByCode(code);
        }

        /// <summary>
        /// 根据商品名获得商品对象  goods
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Goods GetGoodsByName(string name)
        {
            return _goodsDAL.SelectByName(name);
        }

        /// <summary>
        /// 更新商品
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public bool Update(Goods goods)
        {
            return _goodsDAL.Update(goods);
        }
    }
}
