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
    public class CartBLL:ICartBLL
    {
        private ICartDAL _cartDAL;
        public CartBLL(ICartDAL cartDAL)
        {
            _cartDAL = cartDAL;
        }

        /// <summary>
        /// 添加新商品到购物车
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public bool Add(Cart cart)
        {
            return _cartDAL.Insert(cart);
        }

        /// <summary>
        /// 通过memberId 获取cartList
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public IEnumerable<Cart> GetCartByMemberId(Guid memberId)
        {
            return _cartDAL.SelectByMemberId(memberId);
        }

        /// <summary>
        /// 根据会员Id 和 商品id 获取购物车item对象
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public Cart GetCartByMemberIdAndGoodsId(Guid memberId, Guid goodsId)
        {
            return _cartDAL.SelectByMemberIdAndGoodsId(memberId, goodsId);
        }

        /// <summary>
        /// 判断指定用户是否有商品在购物车
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public bool IsAnythingsInCart(Guid memberId)
        {
            return _cartDAL.IsAnythingsInCart(memberId);
        }

        /// <summary>
        /// 判断是否已经添加到了购物车
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public bool IsInCart(Guid memberId, Guid goodsId)
        {
            return _cartDAL.IsInCart(memberId, goodsId);
        }

        /// <summary>
        /// 删除购物车上的东西
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public bool Remove(Cart cart)
        {
            return _cartDAL.Delete(cart);
        }

        /// <summary>
        /// 更新购物车
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public bool Update(Cart cart)
        {
            return _cartDAL.Update(cart);
        }
    }
}
