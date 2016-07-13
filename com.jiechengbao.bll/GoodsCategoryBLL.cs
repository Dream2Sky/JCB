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

        /// <summary>
        /// 根据分类id 获取 goodscategoryList 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public IEnumerable<GoodsCategory> GetGoodsCategoryListByCategoryId(Guid categoryId)
        {
            return _goodsCategoryDAL.SelectGoodsCategoryListByCategoryId(categoryId);
        }

        /// <summary>
        /// 根据商品Id 获取对应商品的分类列表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IEnumerable<GoodsCategory> GetGoodsCategoryListByGoodsId(Guid Id)
        {
            return _goodsCategoryDAL.SelectGoodsCategoryListByGoodsId(Id);
        }

        /// <summary>
        /// 去掉所有和 goodsId 相关的 goodsCategory
        /// 此方法用于更新goodscategory表的时候用
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public bool RemoveGoodsCategoryByGoodsId(Guid goodsId)
        {
            return _goodsCategoryDAL.DeleteByGoodsId(goodsId);
        }

        public bool Update(GoodsCategory gc)
        {
            return _goodsCategoryDAL.Update(gc);
        }
    }
}
