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

namespace com.jiechengbao.admin
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
        }
    }
}
