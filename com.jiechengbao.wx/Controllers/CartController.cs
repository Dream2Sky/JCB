using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using com.jiechengbao.wx.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private IGoodsImagesBLL _goodsImageBLL;
        public CartController(ICartBLL cartBLL, IMemberBLL memberBLL, IGoodsBLL goodsBLL,IGoodsImagesBLL goodsImagesBLL)
        {
            _cartBLL = cartBLL;
            _memberBLL = memberBLL;
            _goodsBLL = goodsBLL;
            _goodsImageBLL = goodsImagesBLL;
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
                Stopwatch sw = new Stopwatch();
                sw.Start();
                goods = _goodsBLL.GetGoodsByCode(goodsCode);
                member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"] as string);

                sw.Stop();
                LogHelper.Log.Write("添加到购物车花了 " + sw.Elapsed);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }

            // 判断是否已经存在购物车上
            if (_cartBLL.IsInCart(member.Id,goods.Id))
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
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"] as string);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            List<Cart> cartList = _cartBLL.GetCartByMemberId(member.Id).ToList();
            sw.Stop();
            LogHelper.Log.Write("根据 用户的Id 查询订单列表 所花时间：" + sw.Elapsed);

            List<CartModel> cartModelList = new List<CartModel>();



            foreach (var item in cartList)

            {
                sw.Restart();
                CartModel cm = new CartModel(_goodsBLL.GetGoodsById(item.GoodsId));
                sw.Stop();
                LogHelper.Log.Write("根据商品id 获得商品 所花的时间 :" + sw.Elapsed);

                sw.Restart();
                
                GoodsImage gi = _goodsImageBLL.GetPictureByGoodsId(item.GoodsId);

                sw.Stop();
                LogHelper.Log.Write("根据商品id 获得对应的图片所花时间:" + sw.Elapsed);

                cm.PicturePath = gi.ImagePath;
                cm.Count = item.Count;

                cartModelList.Add(cm);
            }

            ViewData["CartModelList"] = cartModelList;
            return View();
        }

        /// <summary>
        /// 判断是否有商品在购物车上
        /// </summary>
        /// <returns></returns>
        public ActionResult Check()
        {
            if (_cartBLL.IsAnythingsInCart(_memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"] as string).Id))
            {
                return Json("True", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(string Code)
        {
            if (string.IsNullOrEmpty(Code))
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            Goods goods = _goodsBLL.GetGoodsByCode(Code);
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"] as string);
            Cart cart = _cartBLL.GetCartByMemberIdAndGoodsId(member.Id, goods.Id);

            if (_cartBLL.Remove(cart))
            {
                return Json("True", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }


        }
    }
}