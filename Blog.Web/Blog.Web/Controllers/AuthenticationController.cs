using Blog.Service;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Web.ViewModels.Authentication;
using System.Configuration;
using Blog.Web.Filters;
using Blog.Models;

namespace Blog.Web.Controllers
{
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(BlogControllerContext context)
            : base(context) { }

        protected static LoginViewModel GetViewModel()
        {
            return new LoginViewModel
            {
                OpenIdProviders = new []
                {
                    new OpenIdProvider { Name = "StackExchange", LoginUrl = "https://openid.stackexchange.com" },
                    new OpenIdProvider { Name = "Google", LoginUrl = "https://www.google.com/accounts/o8/id" },
                    new OpenIdProvider { Name = "Yahoo", LoginUrl = "https://me.yahoo.com" }
                }
            };
        }

        protected BlogUser BlogUser
        {
            get
            {
                return User as BlogUser;
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            var openId = new OpenIdRelyingParty();
            var response = openId.GetResponse();
                        
            if (response != null)
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        var user = BlogService.GetOrCreateUser(response);
                        return FinishAuthentication(user);

                    case AuthenticationStatus.Canceled:
                        throw new Exception("Login was cancelled at the provider");

                    case AuthenticationStatus.Failed:
                        throw new Exception("Login failed using the provided OpenID identifier");

                    default:
                        throw new NotImplementedException();
                }
            }

            return View(GetViewModel());
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!Identifier.IsValid(model.IdentityProviderUri))
            {
                throw new Exception("The specified login identifier is invalid");
            }

            var openId = new OpenIdRelyingParty();
            var request = openId.CreateRequest(Identifier.Parse(model.IdentityProviderUri));

            request.AddExtension(new ClaimsRequest
            {
                BirthDate = DemandLevel.NoRequest,
                Email = DemandLevel.Request,
                FullName = DemandLevel.NoRequest,
                TimeZone = DemandLevel.Request,
                Nickname = DemandLevel.Request,
                Country = DemandLevel.NoRequest,
                Gender = DemandLevel.NoRequest,
                Language = DemandLevel.NoRequest,
                PostalCode = DemandLevel.NoRequest
            });

            return request.RedirectingResponse.AsActionResult();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Response.Cookies.Clear();
            Session.Abandon();
            return RedirectToRoute(RouteNames.Main);
        }

        [HttpGet, RequireHttpsNonDebug]
        public ActionResult DirectLogin()
        {
            return View(new DirectLoginViewModel());
        }

        [HttpPost, RequireHttpsNonDebug]
        public ActionResult DirectLogin(DirectLoginViewModel model)
        {
            var user = BlogService.PasswordValidate(model.Upn, model.Password);
            if (user == null)
            {
                return RedirectToAction("DirectLogin");
            }
            return FinishAuthentication(user);
        }

        private ActionResult FinishAuthentication(UserModel user)
        {
            var blogUser = new BlogUser
            {
                Email = user.Email,
                Roles = user.Permissions.Select(a => a.ToString()).ToList(),
                UserId = user.UserId,
                Upn = user.Upn
            };
            SetUserToContext(HttpContext.ApplicationInstance.Context, blogUser);
            return RedirectToRoute(RouteNames.Main);
        }
    }
}
