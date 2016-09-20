using Autofac;
using Autofac.Integration.Mvc;
using com.jiechengbao.bll;
using com.jiechengbao.dal;
using com.jiechengbao.Ibll;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace com.jiechengbao.wx
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            SetupResolveRules(builder);
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        private void SetupResolveRules(ContainerBuilder builder)
        {
            builder.RegisterType<AdminDAL>().As<IAdminDAL>();
            builder.RegisterType<AdminBLL>().As<IAdminBLL>();
            builder.RegisterType<MemberDAL>().As<IMemberDAL>();
            builder.RegisterType<MemberBLL>().As<IMemberBLL>();
            builder.RegisterType<OrderDAL>().As<IOrderDAL>();
            builder.RegisterType<OrderBLL>().As<IOrderBLL>();
            builder.RegisterType<CategoryBLL>().As<ICategoryBLL>();
            builder.RegisterType<CategoryDAL>().As<ICategoryDAL>();
            builder.RegisterType<GoodsBLL>().As<IGoodsBLL>();
            builder.RegisterType<GoodsDAL>().As<IGoodsDAL>();
            builder.RegisterType<GoodsCategoryDAL>().As<IGoodsCategoryDAL>();
            builder.RegisterType<GoodsCategoryBLL>().As<IGoodsCategoryBLL>();
            builder.RegisterType<GoodsImagesBLL>().As<IGoodsImagesBLL>();
            builder.RegisterType<GoodsImagesDAL>().As<IGoodsImagesDAL>();
            builder.RegisterType<CartDAL>().As<ICartDAL>();
            builder.RegisterType<CartBLL>().As<ICartBLL>();
            builder.RegisterType<OrderBLL>().As<IOrderBLL>();
            builder.RegisterType<OrderDAL>().As<IOrderDAL>();
            builder.RegisterType<OrderDetailBLL>().As<IOrderDetailBLL>();
            builder.RegisterType<OrderDetailDAL>().As<IOrderDetailDAL>();
            builder.RegisterType<AddressBLL>().As<IAddressBLL>();
            builder.RegisterType<AddressDAL>().As<IAddressDAL>();
            builder.RegisterType<TransactionBLL>().As<ITransactionBLL>();
            builder.RegisterType<TransactionDAL>().As<ITransactionDAL>();
            builder.RegisterType<RulesBLL>().As<IRulesBLL>();
            builder.RegisterType<RulesDAL>().As<IRulesDAL>();
            builder.RegisterType<ReCommendBLL>().As<IReCommendBLL>();
            builder.RegisterType<ReCommendDAL>().As<IReCommendDAL>();
            builder.RegisterType<CarBLL>().As<ICarBLL>();
            builder.RegisterType<CarDAL>().As<ICarDAL>();
            builder.RegisterType<RechargeBLL>().As<IRechargeBLL>();
            builder.RegisterType<RechargeDAL>().As<IRechargeDAL>();
            builder.RegisterType<CreditRecordBLL>().As<ICreditRecordBLL>();
            builder.RegisterType<CreditRecordDAL>().As<ICreditRecordDAL>();
            builder.RegisterType<ServiceBLL>().As<IServiceBLL>();
            builder.RegisterType<ServiceDAL>().As<IServiceDAL>();
            builder.RegisterType<ServiceConsumePasswordBLL>().As<IServiceConsumePasswordBLL>();
            builder.RegisterType<ServiceConsumePasswordDAL>().As<IServiceConsumePasswordDAL>();
            builder.RegisterType<ServiceConsumeRecordBLL>().As<IServiceConsumeRecordBLL>();
            builder.RegisterType<ServiceConsumeRecordDAL>().As<IServiceConsumeRecordDAL>();
            builder.RegisterType<ServiceQRBLL>().As<IServiceQRBLL>();
            builder.RegisterType<ServiceQRDAL>().As<IServiceQRDAL>();
            builder.RegisterType<ExchangeServiceBLL>().As<IExchangeServiceBLL>();
            builder.RegisterType<ExchangeServiceDAL>().As<IExchangeServiceDAL>();
            builder.RegisterType<ExchangeServiceRecordBLL>().As<IExchangeServiceRecordBLL>();
            builder.RegisterType<ExchangeServiceRecordDAL>().As<IExchangeServiceRecordDAL>();
            builder.RegisterType<ExchangeServiceQRBLL>().As<IExchangeServiceQRBLL>();
            builder.RegisterType<ExchangeServiceQRDAL>().As<IExchangeServiceQRDAL>();
            builder.RegisterType<OrderStatusBLL>().As<IOrderStatusBLL>();
            builder.RegisterType<OrderStatusDAL>().As<IOrderStatusDAL>();
            builder.RegisterType<MyAppointmentBLL>().As<IMyAppointmentBLL>();
            builder.RegisterType<MyAppointmentDAL>().As<IMyAppointmentDAL>();
            builder.RegisterType<MyAppointmentItemBLL>().As<IMyAppointmentItemBLL>();
            builder.RegisterType<MyAppointmentItemDAL>().As<IMyAppointmentItemDAL>();
            builder.RegisterType<AppointmentServiceBLL>().As<IAppointmentServiceBLL>();
            builder.RegisterType<AppointmentServiceDAL>().As<IAppointmentServiceDAL>();
            builder.RegisterType<AppointmentTimeBLL>().As<IAppointmentTimeBLL>();
            builder.RegisterType<AppointmentTimeDAL>().As<IAppointmentTimeDAL>();
            builder.RegisterType<FreeCouponBLL>().As<IFreeCouponBLL>();
            builder.RegisterType<FreeCouponDAL>().As<IFreeCouponDAL>();
            builder.RegisterType<MyFreeCouponBLL>().As<IMyFreeCouponBLL>();
            builder.RegisterType<MyFreeCouponDAL>().As<IMyFreeCouponDAL>();
            builder.RegisterType<AdvertisementBLL>().As<IAdvertisementBLL>();
            builder.RegisterType<AdvertisementDAL>().As<IAdvertisementDAL>();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["SessionID"] = Session.SessionID;
        }

        protected void Session_End(object sender, EventArgs e)
        {
            #region 删除优惠券二维码
            if (System.Web.HttpContext.Current.Session["FreeCouponQrPath"] != null)
            {
                try
                {
                    // get the qr path
                    string path = Server.MapPath("~/MyFreeCouponQRs/" + System.Web.HttpContext.Current.Session["FreeCouponQrPath"].ToString());
                    System.IO.FileInfo fi = new System.IO.FileInfo(path);

                    // delete the qr file when the current session is ending
                    if (fi != null)
                    {
                        fi.Delete();
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Log.Write(ex.Message);
                    LogHelper.Log.Write(ex.StackTrace);
                }

                System.Web.HttpContext.Current.Session["FreeCouponQrPath"] = null;
            }
            #endregion

            #region 删除服务二维码
            if (System.Web.HttpContext.Current.Session["ServiceQRPath"] != null)
            {
                try
                {
                    string path = Server.MapPath("~/QR/" + System.Web.HttpContext.Current.Session["ServiceQRPath"].ToString());
                    System.IO.FileInfo fi = new System.IO.FileInfo(path);
                    if (fi != null)
                    {
                        fi.Delete();
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Log.Write(ex.Message);
                    LogHelper.Log.Write(ex.StackTrace);
                }
            }
            #endregion
            
        }
    }
}
