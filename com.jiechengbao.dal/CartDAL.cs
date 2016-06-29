using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class CartDAL : DataBaseDAL<Cart>, ICartDAL
    {
        public bool IsAnythingsInCart(Guid memberId)
        {
            try
            {
                IEnumerable<Cart> cartList = db.Set<Cart>().Where(n => n.MemberId == memberId).Where(n => n.IsDeleted == false);
                if (cartList == null || cartList.Count() == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        public bool IsInCart(Guid memberId, Guid goodsId)
        {
            try
            {
                IEnumerable<Cart> cartList = db.Set<Cart>().Where(n => n.MemberId == memberId).Where(n => n.GoodsId == goodsId).Where(n=>n.IsDeleted == false);
                if (cartList == null || cartList.Count() == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        public IEnumerable<Cart> SelectByMemberId(Guid memberId)
        {
            try
            {
                return db.Set<Cart>().Where(n => n.MemberId == memberId).Where(n => n.IsDeleted == false);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// 获取状态为 未标记为删除的 购物车对象
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public Cart SelectByMemberIdAndGoodsId(Guid memberId, Guid goodsId)
        {
            try
            {
                return db.Set<Cart>().Where(n => n.MemberId == memberId).Where(n => n.GoodsId == goodsId).Where(n=>n.IsDeleted == false).SingleOrDefault();
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
