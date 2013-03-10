using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Blog.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "About",
                url: "about",
                defaults: new { controller = "Info", action = "About" }
            );
            routes.MapRoute(
                name: "Version",
                url: "Version",
                defaults: new { controller = "Info", action = "VersionInfo" }
            );
            routes.MapRoute(
                name: "Main",
                url: "Main",
                defaults: new { controller = "Home", action = "Main" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Main", id = UrlParameter.Optional }
            );
        }
    }
}