﻿using com.jiechengbao.Ibll;
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
