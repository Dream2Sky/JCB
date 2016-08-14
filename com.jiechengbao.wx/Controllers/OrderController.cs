using com.jiechengbao.common;
using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using com.jiechengbao.wx.Global;
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

        private delegate void DeleteOrderDetailDel(string orderNo);

        private IMemberBLL _memberBLL;
        private IOrderBLL _orderBLL;
        private IOrderDetailBLL _orderDetailBLL;
        private IGoodsBLL _goodsBLL;
        private IAddressBLL _addressBLL;
        private IGoodsImagesBLL _goodsImagesBLL;
        private ICartBLL _cartBLL;
        private IRulesBLL _rulesBLL;
        private IOrderStatusBLL _orderStausBLL;
        private IServiceBLL _serviceBLL;
        public OrderController(IOrderDetailBLL orderDetailBLL, IOrderBLL orderBLL,
            IMemberBLL memberBLL, IGoodsBLL goodsBLL, IAddressBLL addressBLL,
            IGoodsImagesBLL goodsImagesBLL, ICartBLL cartBLL, IRulesBLL rulesBLL,
            IOrderStatusBLL orderStatusBLL, IServiceBLL serviceBLL)
        {
            _orderDetailBLL = orderDetailBLL;
            _orderBLL = orderBLL;
            _memberBLL = memberBLL;
            _goodsBLL = goodsBLL;
            _addressBLL = addressBLL;
            _goodsImagesBLL = goodsImagesBLL;
            _cartBLL = cartBLL;
            _rulesBLL = rulesBLL;
            _orderStausBLL = orderStatusBLL;
            _serviceBLL = serviceBLL;
        }

        [HttpPost]
        public ActionResult Add(int payway)
        {
            // 先判断 各个session是否为空
            if (System.Web.HttpContext.Current.Session["CartModelList"] == null
                // 没有了配送地址  session['Address']就不需要了
                //|| System.Web.HttpContext.Current.Session["Address"] == null
                || System.Web.HttpContext.Current.Session["TotalPrice"] == null || System.Web.HttpContext.Current.Session["TotalCredit"] == null)
            {
                // 如果为空 则返回超时提示
                return Json("Expired", JsonRequestBehavior.AllowGet);
            }

            List<CartModel> cartList = System.Web.HttpContext.Current.Session["CartModelList"] as List<CartModel>;

            // 根据支付方式 选择支付金额
            double TotalPrice = 0;
            if (payway == 0)
            {
                TotalPrice = double.Parse(System.Web.HttpContext.Current.Session["TotalPrice"].ToString());
            }
            else
            {
                TotalPrice = double.Parse(System.Web.HttpContext.Current.Session["TotalCredit"].ToString());
            }


            //需求更改  不要配送地址 所以这里就不用获取Address对象了

            //Address address = System.Web.HttpContext.Current.Session["Address"] as Address;

            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());

            Order order = new Order();
            order.Id = Guid.NewGuid();
            order.IsDeleted = false;
            order.CreatedTime = DateTime.Now;
            order.DeletedTime = DateTime.MinValue.AddHours(8);

            // 还有这个

            //order.AddressId = address.Id;

            order.MemberId = member.Id;
            string gid = Guid.NewGuid().ToString().Replace("-", "").ToUpper().Substring(0, 6);
            order.OrderNo = gid + TimeManager.GetCurrentTimestamp().ToString();
            order.Status = 0;
            order.TotalPrice = TotalPrice;
            order.PayWay = payway;

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
                    od.CurrentCredit = item.ExchangeCredit;
                    od.DeletedTime = DateTime.MinValue.AddHours(8);
                    od.GoodsId = item.Id;
                    od.IsDeleted = false;
                    od.OrderId = order.Id;
                    od.OrderNo = order.OrderNo;
                    od.TotalPrice = od.Count * od.CurrentDiscount * od.CurrentPrice;
                    od.TotalCredit = od.Count * od.CurrentCredit;
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

                // 添加订单成功后 要把购物车上的物品标记成 已删除

                foreach (CartModel item in cartList)
                {
                    Cart cart = new Cart();
                    cart = _cartBLL.GetCartByMemberIdAndGoodsId(member.Id, item.Id);
                    cart.IsDeleted = true;

                    _cartBLL.Update(cart);
                }

                // 坑爹的老板说  不要配送功能

                // 这段代码不要了

                #region 需求更改  不要 配送功能  所以这里的就不要添加配送状态了

                //订单添加后 添加OrderStatus对象 标记订单的配送状态
                //OrderStatus os = new OrderStatus();
                //os.Id = Guid.NewGuid();
                //os.IsDeleted = false;
                //os.OrderId = order.Id;
                //os.Status = 0;
                //os.CreatedTime = DateTime.Now;
                //os.DeletedTime = DateTime.MinValue.AddHours(8);

                //_orderStausBLL.Add(os);
                #endregion


                var jsonResult = new
                {
                    payway = payway,
                    totalprice = order.TotalPrice,
                    orderNo = order.OrderNo
                };
                return Json(jsonResult, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete(string OrderNo)
        {
            if (string.IsNullOrEmpty(OrderNo))
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
            Order order = _orderBLL.GetOrderByOrderNo(OrderNo);
            if (order == null)
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            order.IsDeleted = true;
            order.DeletedTime = DateTime.Now;

            if (_orderBLL.Update(order))
            {
                // 删除一条订单时  异步删除订单下的订单详情
                DeleteOrderDetailDel del = new DeleteOrderDetailDel(DeleteOrderDetail);
                IAsyncResult ar = del.BeginInvoke(order.OrderNo, Callback, null);

                return Json("True", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
        }

        //[HttpPost]
        //public ActionResult Create()
        //{
        //    return View();
        //}
        public ActionResult Detail(string orderNo)
        {
            // 先获得订单对象
            Order order = _orderBLL.GetOrderByOrderNo(orderNo);
            ViewBag.OrderNo = order.OrderNo;
            ViewBag.OrderStatus = order.Status;
            ViewBag.Status = order.Status == 0 ? "未完成" : (order.Status == 1 ? "已完成" : "已取消");
            ViewBag.TotalPrice = order.TotalPrice;
            ViewBag.CreateTime = order.CreatedTime;
            ViewBag.Payway = order.PayWay == 0 ? "微信支付" : "积分支付";

            // 没了  配送功能没了 所以这个订单配送状态就没了

            // 也不用显示地址了 

            #region 没有配送功能后的改动
            // 获取订单配送状态
            //OrderStatus os = _orderStausBLL.GetOrderStatusByOrderId(order.Id);
            //ViewBag.Logistical = os.Status;


            // 获取地址
            // Address address = _addressBLL.GetAddressById(order.AddressId);

            // 构造 AddressModel
            //AddressModel ad = new AddressModel();
            //ad.City = address.City;
            //ad.Consignee = address.Consignee;
            //ad.County = address.County;
            //ad.Detail = address.Detail;
            //ad.Phone = address.Phone;
            //ad.Province = address.Province;

            //ViewData["address"] = ad;
            #endregion 


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
            return View();
        }

        [HttpPost]
        public PartialViewResult List(int type)
        {
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());
            LogHelper.Log.Write("memberId is " + member.Id);
            // 临时的订单列表
            List<Order> orderList = new List<Order>();

            // 要提交的 OrderModel 列表
            List<OrderModel> omList = new List<OrderModel>();
            
            if (type == 2)
            {
                orderList = _orderBLL.GetAllOrders(member.Id).ToList();
                omList = CreateOrderModelList(orderList);
            }
            else if (type == 1)
            {
                orderList = _orderBLL.GetOrdersByStatus(member.Id, 1).ToList();
                omList = CreateOrderModelList(orderList);
            }
            else if (type == 0)
            {
                orderList = _orderBLL.GetOrdersByStatus(member.Id, 0).ToList();
                omList = CreateOrderModelList(orderList);
            }
            else
            {
                orderList = _orderBLL.GetAllOrders(member.Id).ToList();
                omList = CreateOrderModelList(orderList);
            }

            ViewData["OrderList"] = omList;
            return PartialView();
        }

        [IsLogin]
        [IsRegister]
        public ActionResult OrderList()
        {
            List(2);
            return View();
        }

        /// <summary>
        /// 构造 orderModelList 的私有方法
        /// </summary>
        /// <param name="orderList"></param>
        /// <returns></returns>
        [NonAction]
        private List<OrderModel> CreateOrderModelList(List<Order> orderList)
        {
            // 要返回的 OrderModel 列表
            List<OrderModel> omList = new List<OrderModel>();

            foreach (var item in orderList)
            {
                OrderModel om = new OrderModel(item);
                List<OrderDetailModel> odmList = new List<OrderDetailModel>();
                foreach (OrderDetail model in _orderDetailBLL.GetOrderDetailByOrderNo(item.OrderNo))
                {
                    OrderDetailModel orm = new OrderDetailModel(model);
                    Goods good = _goodsBLL.GetGoodsById(model.GoodsId);
                    orm.GoodsName = good.Name;
                    orm.PicturePath = _goodsImagesBLL.GetPictureByGoodsId(model.GoodsId).ImagePath;
                    orm.Description = good.Description;
                    orm.GoodsCode = good.Code;

                    odmList.Add(orm);
                }
                om.GoodsModelList = odmList;
                omList.Add(om);
            }

            return omList;
        }

        /// <summary>
        ///  填写订单
        ///  从购物车中获取和构造订单数据 并 保存到 session中
        /// </summary>
        /// <returns></returns>
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
                    double discount = _rulesBLL.GetDiscountByVIP(member.Vip);

                    // 没有配送功能

                    // 所以订单就不用绑定地址
                    #region 没有配送功能的改动

                    //Address address = new Address();
                    //// 判断是否绑定了配送地址
                    //if (!_addressBLL.IsBindAddress(member.Id))
                    //{
                    //    return Json("NoAddress", JsonRequestBehavior.AllowGet);
                    //}
                    //else
                    //{
                    //    // 找到默认配送地址 或者是 第一个地址
                    //    address = _addressBLL.GetDefaultOrFirstAddress(member.Id);
                    //}

                    #endregion


                    double TotalPrice = 0;
                    double TotalCredit = 0;
                    List<CartModel> cartList = new List<CartModel>();

                    JArray ja = (JArray)JsonConvert.DeserializeObject(json);
                    for (int i = 0; i < ja.Count; i++)
                    {
                        CartModel cm = new CartModel(_goodsBLL.GetGoodsById(Guid.Parse(ja[i]["Id"].ToString())));
                        cm.Count = int.Parse(ja[i]["Count"].ToString());
                        cm.PicturePath = _goodsImagesBLL.GetPictureByGoodsId(Guid.Parse(ja[i]["Id"].ToString())).ImagePath;
                        cm.Discount = discount;
                        TotalPrice += (cm.Price * cm.Count * cm.Discount);
                        TotalCredit += (cm.ExchangeCredit * cm.Count);
                        cartList.Add(cm);
                    }

                    // 获得 在购物车上选择的商品列表 包括数量
                    ViewData["CartModelList"] = cartList;
                    ViewBag.TotalPrice = TotalPrice;
                    ViewBag.TotalCredit = TotalCredit;
                    // 去掉配送地址

                    // ViewBag.Address = address;

                    // 备份临时数据 如果用户在修改配送地址的时候可以用到
                    System.Web.HttpContext.Current.Session["CartModelList"] = cartList;
                    System.Web.HttpContext.Current.Session["TotalPrice"] = TotalPrice;
                    System.Web.HttpContext.Current.Session["TotalCredit"] = TotalCredit;
                    // 没有了 session['address'] 

                    // System.Web.HttpContext.Current.Session["Address"] = address;
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

        /// <summary>
        /// 填写订单页
        /// </summary>
        /// <returns></returns>
        public ActionResult Write()
        {
            // 没有了配送功能
            // Session['Address'] 就不能要了

            #region 没有配送功能之后的改动

            // 判断传递进来的AddressId是否为空 

            // 如果为空这表示不是从编辑地址页返回

            // 所以就直接用在 write 里面保存好的 Address
            //if (Request.QueryString["AddressId"] == null)
            //{
            //    ViewBag.Address = System.Web.HttpContext.Current.Session["Address"] as Address;
            //}
            //else
            //{
            //    // 如果不为空 说明是从编辑地址页 传来的

            //    // 所以要用新的 Address对象
            //    //Address address = _addressBLL.GetAddressById(Guid.Parse(Request.QueryString["AddressId"].ToString()));
            //    //System.Web.HttpContext.Current.Session["Address"] = address;
            //    //ViewBag.Address = address;
            //}
            #endregion


            ViewData["CartModelList"] = System.Web.HttpContext.Current.Session["CartModelList"] as List<CartModel>;
            ViewBag.TotalPrice = System.Web.HttpContext.Current.Session["TotalPrice"];
            ViewBag.TotalCredit = System.Web.HttpContext.Current.Session["TotalCredit"];

            return View();
        }

        /// <summary>
        ///  监测是否有未完成的订单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Check()
        {
            if (_orderBLL.HasUncompletedOrders(_memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString()).Id))
            {
                return Json("True", JsonRequestBehavior.AllowGet);
            }
            return Json("False", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 继续支付
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ContinuePay(string orderNo)
        {
            if (string.IsNullOrEmpty(orderNo))
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            Order order = _orderBLL.GetOrderByOrderNo(orderNo);

            if (order == null)
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            var orderInfo = new
            {
                payway = order.PayWay,
                totalprice = order.TotalPrice,
                orderNo = order.OrderNo
            };

            return Json(orderInfo, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        private void DeleteOrderDetail(string orderNo)
        {
            List<OrderDetail> odList = _orderDetailBLL.GetOrderDetailByOrderNo(orderNo).ToList();

            foreach (var item in odList)
            {
                item.IsDeleted = true;
                item.DeletedTime = DateTime.Now;

                _orderDetailBLL.Update(item);
            }
        }

        [NonAction]
        private void Callback(IAsyncResult ar)
        {
            try
            {
                DeleteOrderDetailDel del = (DeleteOrderDetailDel)ar.AsyncState;
                del.EndInvoke(ar);
            }
            catch (Exception)
            {
            }
        }

        

        // 去除配送状态的修改
        // 因为老板说不要配送功能了!!!

        #region 没有了配送功能  自然就没有了配送状态了

        ///// <summary>
        ///// 修改订单配送状态
        ///// </summary>
        ///// <param name="orderNo"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult Shipping(string orderNo)
        //{
        //    if (string.IsNullOrEmpty(orderNo))
        //    {
        //        return Json("False", JsonRequestBehavior.AllowGet);
        //    }

        //    Order order = _orderBLL.GetOrderByOrderNo(orderNo);
        //    if (order == null)
        //    {
        //        return Json("False", JsonRequestBehavior.AllowGet);
        //    }

        //    OrderStatus os = _orderStausBLL.GetOrderStatusByOrderId(order.Id);

        //    if (os == null)
        //    {
        //        return Json("False", JsonRequestBehavior.AllowGet);
        //    }

        //    os.Status = 2;
        //    if (_orderStausBLL.Update(os))
        //    {
        //        return Json("True", JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json("False", JsonRequestBehavior.AllowGet);
        //    }
        //}

        #endregion
    }

}