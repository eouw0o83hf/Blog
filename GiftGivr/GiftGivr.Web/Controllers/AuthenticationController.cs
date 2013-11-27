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
        private const int HashIterations = 10000;
        private const int SaltSize = 128;

        public AuthenticationController(GiftGivrControllerContext context)
            : base(context) { }

        [HttpGet]
        public ActionResult GenerateTestLogin()
        {
            if (!Request.Url.AbsoluteUri.ToString().Contains("http://localhost:"))
            {
                throw new Exception("NOPE!");
            }

            var email = Guid.NewGuid().ToString();
            var salt = GetNewSalt();
            var hash = HashPassword("a", salt);

            var account = new Data.Account
            {
                Email = email,
                Salt = salt,
                Password = hash,
                Name = "a test"
            };
            DataContext.Accounts.InsertOnSubmit(account);
            DataContext.SubmitChanges();

            return View(new LoginViewModel
            {
                Email = email,
                Password = "a"
            });
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
                var hash = HashPassword(model.Password, target.Salt);
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

        protected string GetNewSalt()
        {
            return CryptoService.GenerateSalt(HashIterations, SaltSize);
        }

        protected string HashPassword(string password, string salt)
        {
            return CryptoService.Compute(password, salt); 
        }
    }
}
