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
        public bool IsInCart(Guid memberId, Guid goodsId)
        {
            try
            {
                IEnumerable<Cart> cartList = db.Set<Cart>().Where(n => n.MemberId == memberId).Where(n => n.GoodsId == goodsId);
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

        public Cart SelectByMemberIdAndGoodsId(Guid memberId, Guid goodsId)
        {
            try
            {
                return db.Set<Cart>().Where(n => n.MemberId == memberId).Where(n => n.GoodsId == goodsId).FirstOrDefault();
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
