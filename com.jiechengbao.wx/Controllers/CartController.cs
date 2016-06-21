using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.wx.Controllers
{
    public class CartController:Controller
    {
        private ICartBLL _cartBLL;
        private IMemberBLL _memberBLL;
        private IGoodsBLL _goodsBLL;
        public CartController(ICartBLL cartBLL, IMemberBLL memberBLL, IGoodsBLL goodsBLL)
        {
            _cartBLL = cartBLL;
            _memberBLL = memberBLL;
            _goodsBLL = goodsBLL;
        }

        /// <summary>
        /// 添加到购物车
        /// </summary>
        /// <param name="goodsCode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(string goodsCode)
        {
            //先获得 goods 和 member 对象
            Goods goods = new Goods();
            Member member = new Member();
            try
            {
                goods = _goodsBLL.GetGoodsByCode(goodsCode);
                member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"] as string);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }

            // 判断是否已经存在购物车上
            if (!_cartBLL.IsInCart(member.Id,goods.Id))
            {
                // 不在购物车上时 直接添加到购物车上
                Cart cart = new Cart();
                cart.Id = Guid.NewGuid();
                cart.Count = 1;
                cart.CreatedTime = DateTime.Now.Date;
                cart.DeletedTime = DateTime.MinValue.AddHours(8);
                cart.GoodsId = goods.Id;
                cart.MemberId = member.Id;
                cart.IsDeleted = false;

                _cartBLL.Add(cart);

                return Json("True", JsonRequestBehavior.AllowGet);
            }
            else
            {
                // 判断存在购物车上时  就取出 Cart 对象
                // 修改 cart 的数量
                Cart cart = _cartBLL.GetCartByMemberIdAndGoodsId(member.Id, goods.Id);
                cart.Count += 1;

                _cartBLL.Update(cart);
                return Json("True", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult List()
        {
            return View();
        }
    }
}