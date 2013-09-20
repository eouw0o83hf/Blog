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

        public const string Blog = "Blog";
        public const string Feed = "Feed";
        public const string FeedDefault = "FeedDefault";
        public const string Permalink = "Permalink";

        public const string AccountIndex = "Account";
        public const string ConfirmEmail = "ConfirmEmail";

        public const string Admin = "Admin";

        public const string Login = "Login";
        public const string Logout = "Logout";
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
                name: RouteNames.Admin,
                url: "Admin",
                defaults: new { controller = "Admin", action = "Index" }
            );

            routes.MapRoute(
                name: RouteNames.Blog,
                url: "blog/{blog}",
                defaults: new { controller = "Blog", action = "Page" }
            );

            routes.MapRoute(
                name: RouteNames.Feed,
                url: "blog/{blog}/feed",
                defaults: new { controller = "Feed", action = "Index" }
            );

            routes.MapRoute(
                name: RouteNames.FeedDefault,
                url: "feed",
                defaults: new { controller = "Feed", action = "Index" }
            );

            routes.MapRoute(
                name: RouteNames.AccountIndex,
                url: "account",
                defaults: new { controller = "Account", action = "Index" }
            );

            routes.MapRoute(
                name: RouteNames.ConfirmEmail,
                url: "confirmemail/{id}",
                defaults: new { controller = "Account", action = "VerifyEmail" }
            );

            routes.MapRoute(
                name: RouteNames.Login,
                url: "login",
                defaults: new { controller = "Authentication", action = "Login" }
            );

            routes.MapRoute(
                name: RouteNames.Logout,
                url: "logout",
                defaults: new { controller = "Authentication", action = "Logout" }
            );

            routes.MapRoute(
                name: RouteNames.Permalink,
                url: "permalink/{postIdentifier}/{urlTitle}",
                defaults: new { controller = "Blog", action = "Permalink", urlTitle = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: RouteNames.Default,
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Blog", action = "Page", id = UrlParameter.Optional }
            );
        }
    }
}