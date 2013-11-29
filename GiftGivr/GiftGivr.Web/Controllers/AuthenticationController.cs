using GiftGivr.Web.Classes;
using GiftGivr.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GiftGivr.Web.Controllers
{
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(GiftGivrControllerContext context)
            : base(context) { }

        [HttpGet]
        public ActionResult GenerateTestLogin()
        {
            if (!Request.Url.AbsoluteUri.ToString().Contains("http://localhost:"))
            {
                throw new Exception("NOPE!");
            }

            return View(new TestUserViewModel());
        }

        [HttpPost]
        public ActionResult GenerateTestLogin(TestUserViewModel model)
        {
            if (!Request.Url.AbsoluteUri.ToString().Contains("http://localhost:"))
            {
                throw new Exception("NOPE!");
            }

            var salt = CryptoProvider.GetNewSalt();
            var hash = CryptoProvider.HashPassword(model.Password, salt);

            var account = new Data.Account
            {
                Email = model.Email,
                Salt = salt,
                Password = hash,
                Name = model.Name
            };
            DataContext.Accounts.InsertOnSubmit(account);
            DataContext.SubmitChanges();

            return Redirect("/");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var target = DataContext.Accounts.FirstOrDefault(a => a.Email == model.Email);
            if (target != null)
            {
                var hash = CryptoProvider.HashPassword(model.Password, target.Salt);
                if (hash == target.Password)
                {
                    FormsAuthentication.RedirectFromLoginPage(target.AccountId.ToString(), false);
                    return RedirectToAction("Index", "Home");
                }
            }

            TempData["Message"] = "Login failed";
            FormsAuthentication.RedirectToLoginPage();
            return null;
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}
