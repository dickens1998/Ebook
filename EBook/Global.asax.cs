 using Autofac;
using Autofac.Integration.Mvc;
using EBook.App_Start;
using EBook.Core.Autofac;
using EBook.Filters;
using EBook.Services.Autofac;
using Serilog;
using System.Web.Mvc;
using System.Web.Routing;


namespace EBook
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //注册全局过滤器
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            // 创建一个容器
            var builder = new ContainerBuilder();

            // 注册Service
            builder.RegisterModule<ServiceModule>();

            builder.RegisterModule<DataModule>();

            builder.RegisterModule<LoggerModule>();

            builder.RegisterFilterProvider();

            builder.Register(c => new ActionFilter(c.ResolveKeyed<ILogger>("mvc_stopwatch"))).AsActionFilterFor<Controller>().InstancePerLifetimeScope();

            builder.Register(c => new ExceptionFilter(c.ResolveKeyed<ILogger>("mvc_exction"))).AsExceptionFilterFor<Controller>().InstancePerLifetimeScope();

            // 注册所有的Controller
            builder.RegisterControllers(typeof(MvcApplication).Assembly);


            //设置依赖性解析器
            var container = builder.Build();  //把ContainerBuilder对象创建
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


        }
    }
}
