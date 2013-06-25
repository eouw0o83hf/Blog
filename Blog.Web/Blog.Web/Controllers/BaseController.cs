using Blog.Models;
using Blog.Service;
using Blog.Web.ViewModels.Shared;
using MarkdownSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Blog.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly BlogControllerContext _context;
        protected readonly IBlogService BlogService;
        protected BaseController(BlogControllerContext context)
        {
            _context = context;

            BlogService = context.BlogService;
        }

        #region Events

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            SetUserFromCookie(filterContext.HttpContext.ApplicationInstance.Context);
            base.OnAuthorization(filterContext);
        }

        #endregion

        #region Authentication

        protected static void SetUserFromCookie(HttpContext context)
        {
            var authCookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
            BlogUser result;
            if (authCookie == null)
            {
                result = BlogUser.Unauthenticated;
            }
            else
            {
                try
                {
                    var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    var serializer = new JavaScriptSerializer();
                    result = serializer.Deserialize<BlogUser>(ticket.UserData);
                }
                catch
                {
                    result = BlogUser.Unauthenticated;
                }
            }
            context.User = result;
        }

        protected static void SetUserToContext(HttpContext context, BlogUser user)
        {
            var serializer = new JavaScriptSerializer();
            var userData = serializer.Serialize(user);
            var ticket = new FormsAuthenticationTicket(1, user.Identity.Name, DateTime.UtcNow, DateTime.UtcNow.AddDays(2), true, userData);
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            context.Response.Cookies.Add(cookie);
        }

        #endregion

        #region Helpers
        
        protected virtual TimeZoneInfo GetLocalTime()
        {
            return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
        }

        #endregion

        #region ViewData Cramming

        public const string VIEWDATA_MARKDOWN = "VIEWDATA_MARKDOWN";
        private Markdown GetMarkdown()
        {
            return new Markdown();
        }

        protected virtual void CramViewData()
        {
            ViewData[VIEWDATA_MARKDOWN] = GetMarkdown();

            var menuLinks = new List<LinkViewModel>();

            menuLinks.Add(new LinkViewModel { Url = Url.RouteUrl(RouteNames.Main), LinkText = "Main" });
            menuLinks.Add(new LinkViewModel { Url = Url.RouteUrl(RouteNames.About), LinkText = "About" });
            menuLinks.Add(new LinkViewModel { Url = Url.RouteUrl(RouteNames.Version), LinkText = "Version Info" });
            menuLinks.Add(new LinkViewModel { Url = "https://trello.com/board/eouw0o83hf-com/513f9eae3db9e83015000ce1", LinkText = "Site Development Board" });

            if (User.IsInRole(PermissionEnum.Admin.ToString()))
            {
                menuLinks.Add(new LinkViewModel { Url = Url.RouteUrl(RouteNames.Admin), LinkText = "Admin" });
            }

            ViewData["MenuLinks"] = menuLinks;
        }

        #region Decorators

        protected override ViewResult View(IView view, object model)
        {
            CramViewData();
            return base.View(view, model);
        }

        protected override ViewResult View(string viewName, string masterName, object model)
        {
            CramViewData();
            return base.View(viewName, masterName, model);
        }

        protected override PartialViewResult PartialView(string viewName, object model)
        {
            CramViewData();
            return base.PartialView(viewName, model);
        }

        #endregion

        #endregion
    }
}
