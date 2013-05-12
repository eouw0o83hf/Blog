﻿using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Web.ViewModels.Authentication;

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
                        var blogUser = new BlogUser
                        {
                            Email = user.Email,
                            Roles = new List<string>(),
                            UserId = user.UserId,
                            Upn = user.Upn
                        };
                        SetUserToContext(HttpContext.ApplicationInstance.Context, blogUser);
                        return new EmptyResult();

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
            Session.Abandon();
            return RedirectToRoute(RouteNames.Main);
        }
    }
}
