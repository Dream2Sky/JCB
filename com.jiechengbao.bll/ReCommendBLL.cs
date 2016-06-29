using com.jiechengbao.Ibll;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.jiechengbao.entity;

namespace com.jiechengbao.bll
{
    public class ReCommendBLL:IReCommendBLL
    {
        private IReCommendDAL _recommendDAL;
        public ReCommendBLL(IReCommendDAL recommendDAL)
        {
            _recommendDAL = recommendDAL;
        }

        public bool Add(ReCommend commend)
        {
            return _recommendDAL.Insert(commend);
        }

        /// <summary>
        /// 根据时间大小排序 推荐商品
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ReCommend> GetAllReCommendListwithSortByTime()
        {
            return _recommendDAL.SelectAll().OrderByDescending(n => n.CreatedTime);
        }

        /// <summary>
        /// 判断是否是推荐商品
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public bool IsRecommend(Guid goodsId)
        {
            if (_recommendDAL.SelectByGoodsId(goodsId) == null)
            {
                return false; 
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 根据商品id 删除推荐商品
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public bool Remove(Guid goodsId)
        {
            return _recommendDAL.Delete(_recommendDAL.SelectByGoodsId(goodsId));
        }
    }
}
