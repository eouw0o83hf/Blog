using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Blog.Web
{
    public static class RouteNames
    {
        public const string About = "About";
        public const string Version = "Version";
        public const string Main = "Main";
        public const string Default = "Default";
    }

    public class RouteConfig
    {

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute(
                name: RouteNames.About,
                url: "about",
                defaults: new { controller = "Info", action = "About" }
            );
            routes.MapRoute(
                name: RouteNames.Version,
                url: "Version",
                defaults: new { controller = "Info", action = "VersionInfo" }
            );
            routes.MapRoute(
                name: RouteNames.Main,
                url: "Main",
                defaults: new { controller = "Blog", action = "Page" }
            );

            routes.MapRoute(
                name: RouteNames.Default,
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Blog", action = "Page", id = UrlParameter.Optional }
            );
        }
    }
}