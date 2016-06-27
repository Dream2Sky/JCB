using com.jiechengbao.common;
using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using com.jiechengbao.wx.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.wx.Controllers
{
    public class OrderController : Controller
    {
        private IMemberBLL _memberBLL;
        private IOrderBLL _orderBLL;
        private IOrderDetailBLL _orderDetailBLL;
        private IGoodsBLL _goodsBLL;
        private IAddressBLL _addressBLL;
        private IGoodsImagesBLL _goodsImagesBLL;
        public OrderController(IOrderDetailBLL orderDetailBLL, IOrderBLL orderBLL, IMemberBLL memberBLL, IGoodsBLL goodsBLL, IAddressBLL addressBLL, IGoodsImagesBLL goodsImagesBLL)
        {
            _orderDetailBLL = orderDetailBLL;
            _orderBLL = orderBLL;
            _memberBLL = memberBLL;
            _goodsBLL = goodsBLL;
            _addressBLL = addressBLL;
            _goodsImagesBLL = goodsImagesBLL;
        }

        [HttpPost]
        public ActionResult Add()
        {
            // 先判断 各个session是否为空
            if (System.Web.HttpContext.Current.Session["CartModelList"] == null 
                || System.Web.HttpContext.Current.Session["Address"] == null 
                || System.Web.HttpContext.Current.Session["TotalPrice"] == null)
            {
                // 如果为空 则返回超时提示
                return Json("Expired", JsonRequestBehavior.AllowGet);
            }

            List<CartModel> cartList = System.Web.HttpContext.Current.Session["CartModelList"] as List<CartModel>;
            double TotalPrice = double.Parse(System.Web.HttpContext.Current.Session["TotalPrice"].ToString());
            Address address = System.Web.HttpContext.Current.Session["Address"] as Address;

            Order order = new Order();
            order.Id = Guid.NewGuid();
            order.IsDeleted = false;
            order.CreatedTime = DateTime.Now;
            order.DeletedTime = DateTime.MinValue.AddHours(8);
            order.AddressId = address.Id;
            order.MemberId = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString()).Id;
            string gid = Guid.NewGuid().ToString().Replace("-", "");
            order.OrderNo = gid + TimeManager.GetCurrentTimestamp().ToString();
            order.Status = 0;
            order.TotalPrice = TotalPrice;

            List<OrderDetail> odList = new List<OrderDetail>();

            if (_orderBLL.Add(order))
            {
                foreach (var item in cartList)
                {
                    OrderDetail od = new OrderDetail();
                    od.Id = Guid.NewGuid();
                    od.Count = item.Count;
                    od.CreatedTime = DateTime.Now;
                    od.CurrentDiscount = item.Discount;
                    od.CurrentPrice = item.Price;
                    od.DeletedTime = DateTime.MinValue.AddHours(8);
                    od.GoodsId = item.Id;
                    od.IsDeleted = false;
                    od.OrderId = order.Id;
                    od.OrderNo = order.OrderNo;
                    od.TotalPrice = od.Count * od.CurrentDiscount * od.CurrentPrice;

                    // 添加订单详情

                    // 如果添加失败 则回滚

                    if (!_orderDetailBLL.Add(od))
                    {
                        // 删除已添加的 OrderDetail
                        _orderDetailBLL.Remove(odList);
                        return Json("Error", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        odList.Add(od);
                    }
                }
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        //[HttpPost]
        //public ActionResult Create()
        //{
        //    return View();
        //}

        public ActionResult Detail(string orderNo)
        {
            Order order = _orderBLL.GetOrderByOrderNo(orderNo);

            Address address = _addressBLL.GetAddressById(order.AddressId);
            ViewData["address"] = address;

            List<OrderDetail> odList = new List<OrderDetail>();
            odList = _orderDetailBLL.GetOrderDetailByOrderNo(orderNo).ToList();

            List<OrderDetailModel> odmList = new List<OrderDetailModel>();

            foreach (var item in odList)
            {
                OrderDetailModel odm = new OrderDetailModel(item);
                odm.GoodsName = _goodsBLL.GetGoodsById(item.GoodsId).Name;
                odm.PicturePath = _goodsImagesBLL.GetPictureByGoodsId(item.GoodsId).ImagePath;

                odmList.Add(odm);
            }
            ViewData["OrderDetailModelList"] = odmList;
            ViewBag.TotalPrice = order.TotalPrice;
            return View();
        }

        /// <summary>
        /// 订单列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"] as string);
            List<Order> uncompletedorderList = _orderBLL.GetUnCompletedOrders(member.Id).ToList();
            ViewData["UnCompletedOrderList"] = uncompletedorderList;

            List<Order> completedorderList = _orderBLL.GetCompletedOrders(member.Id).ToList();
            ViewData["CompletedOrderList"] = completedorderList;
            return View();
        }

        [HttpPost]
        public ActionResult WriteOrder()
        {
            #region Ajax提交
            if (Request.IsAjaxRequest())
            {
                var stream = HttpContext.Request.InputStream;
                string json = new StreamReader(stream).ReadToEnd();

                try
                {
                    Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"] as string);
                    Address address = new Address();
                    // 判断是否绑定了配送地址
                    if (!_addressBLL.IsBindAddress(member.Id))
                    {
                        return Json("NoAddress", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        // 找到默认配送地址 或者是 第一个地址
                        address = _addressBLL.GetDefaultOrFirstAddress(member.Id);
                    }

                    double TotalPrice = 0;
                    List<CartModel> cartList = new List<CartModel>();

                    JArray ja = (JArray)JsonConvert.DeserializeObject(json);
                    for (int i = 0; i < ja.Count; i++)
                    {
                        CartModel cm = new CartModel(_goodsBLL.GetGoodsById(Guid.Parse(ja[i]["Id"].ToString())));
                        cm.Count = int.Parse(ja[i]["Count"].ToString());
                        cm.PicturePath = _goodsImagesBLL.GetPictureByGoodsId(Guid.Parse(ja[i]["Id"].ToString())).ImagePath;
                        TotalPrice += (cm.Price * cm.Count * cm.Discount);
                        cartList.Add(cm);
                    }

                    // 获得 在购物车上选择的商品列表 包括数量
                    ViewData["CartModelList"] = cartList;
                    ViewBag.TotalPrice = TotalPrice;
                    ViewBag.Address = address;

                    // 备份临时数据 如果用户在修改配送地址的时候可以用到
                    System.Web.HttpContext.Current.Session["CartModelList"] = cartList;
                    System.Web.HttpContext.Current.Session["TotalPrice"] = TotalPrice;
                    System.Web.HttpContext.Current.Session["Address"] = address;
                }
                catch (Exception ex)
                {
                    LogHelper.Log.Write(ex.Message);
                    LogHelper.Log.Write(ex.StackTrace);
                    throw;
                }
            }
            #endregion
            return Json("True", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Write()
        {
            // 判断传递进来的AddressId是否为空 
            
            // 如果为空这表示不是从编辑地址页返回

            // 所以就直接用在 write 里面保存好的 Address
            if (Request.QueryString["AddressId"] == null)
            {
                ViewBag.Address = System.Web.HttpContext.Current.Session["Address"] as Address;
            }
            else
            {
                // 如果不为空 说明是从编辑地址页 传来的
                
                // 所以要用新的 Address对象
                Address address = _addressBLL.GetAddressById(Guid.Parse(Request.QueryString["AddressId"].ToString()));
                System.Web.HttpContext.Current.Session["Address"] = address;
                ViewBag.Address = address;
            }

            ViewData["CartModelList"] = System.Web.HttpContext.Current.Session["CartModelList"] as List<CartModel>;
            ViewBag.TotalPrice = System.Web.HttpContext.Current.Session["TotalPrice"];

            return View();
        }

    }
}