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
            if (Request.IsAjaxRequest())
            {
                var stream = HttpContext.Request.InputStream;
                string json = new StreamReader(stream).ReadToEnd();

                try
                {
                    Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"] as string);
                    Order order = new Order();
                    order.Id = Guid.NewGuid();
                    order.CreatedTime = DateTime.Now.Date;
                    order.DeletedTime = DateTime.MinValue.AddHours(8);
                    order.IsDeleted = false;
                    order.MemberId = member.Id;
                    order.OrderNo = TimeManager.GetCurrentTimestamp() + "_" + (new Random().Next(0, 255));
                    order.PayTime = DateTime.MinValue.AddHours(8);
                    order.Status = 0;

                    //添加新订单
                    if (_orderBLL.Add(order))
                    {
                        JArray ja = (JArray)JsonConvert.DeserializeObject(json);
                        for (int i = 0; i < ja.Count; i++)
                        {
                            Goods goods = _goodsBLL.GetGoodsById(Guid.Parse(ja[i]["Id"].ToString()));

                            OrderDetail od = new OrderDetail();
                            od.Id = Guid.NewGuid();

                            od.GoodsId = goods.Id;
                            od.Count = int.Parse(ja[i]["Count"].ToString());
                            od.OrderId = order.Id;
                            od.OrderNo = order.OrderNo;

                            od.CurrentPrice = goods.Price;

                            od.CurrentDiscount = goods.Discount;
                            od.IsDeleted = false;
                            od.DeletedTime = DateTime.MinValue.AddHours(8);
                            od.CreatedTime = DateTime.Now.Date;
                            od.TotalPrice = od.CurrentDiscount * od.CurrentPrice * od.Count;

                            order.TotalPrice += od.TotalPrice;
                            _orderDetailBLL.Add(od);

                        }

                        List<Address> addressList = new List<Address>();
                        addressList = _addressBLL.GetAddressByMemberId(member.Id).ToList();

                        if (addressList != null && addressList.Count() > 0)
                        {
                            Address address = addressList.First();
                            order.AddressId = address.Id;
                            ViewData["Address"] = address;

                            _orderBLL.Update(order);
                        }

                        // 添加成功后跳转到 订单的详细页面
                        return Json("True", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("False", JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Log.Write(ex.Message);
                    LogHelper.Log.Write(ex.StackTrace);
                    throw;
                }

            }
            return Json("False", JsonRequestBehavior.AllowGet);
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
                ViewBag.Address = address;
            }

            ViewData["CartModelList"] = System.Web.HttpContext.Current.Session["CartModelList"] as List<CartModel>;
            ViewBag.TotalPrice = System.Web.HttpContext.Current.Session["TotalPrice"];

            return View();
        }

    }
}