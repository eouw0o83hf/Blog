using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Blog.Web
{
    public class MvcApplication : HttpApplication
    {
        protected static IWindsorContainer Container { get; private set; }

        protected static void BootstrapContainer()
        {
            Container = new WindsorContainer();
            Container.Install(new BlogWindsorInstaller());
        }

        protected void Application_Start()
        {
            BootstrapContainer();
            MigrationRunner.RunMigrations(Container);

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var controllerFactory = Container.Resolve<IControllerFactory>();
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        protected void Application_End()
        {
            Container.Dispose();
        }
    }
}