using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class GoodsCategoryDAL : DataBaseDAL<GoodsCategory>, IGoodsCategoryDAL
    {
        /// <summary>
        /// 删除 goodsId 的相关 goodscategory List
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public bool DeleteByGoodsId(Guid goodsId)
        {
            try
            {
                db.Set<GoodsCategory>().RemoveRange(db.Set<GoodsCategory>().Where(n => n.GoodsId == goodsId));
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        public IEnumerable<GoodsCategory> SelectGoodsCategoryListByGoodsId(Guid goodsId)
        {
            try
            {
                return db.Set<GoodsCategory>().Where(n => n.GoodsId == goodsId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }
    }
}
