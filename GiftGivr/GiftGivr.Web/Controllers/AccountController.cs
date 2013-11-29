using GiftGivr.Web.Classes;
using GiftGivr.Web.Data;
using GiftGivr.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GiftGivr.Web.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(GiftGivrControllerContext context)
            : base(context) { }

        [HttpGet]
        public ActionResult Add()
        {
            return View(new AddAccountViewModel());
        }

        [HttpPost]
        public ActionResult Add(AddAccountViewModel model)
        {
            var email = model.Email.Trim();
            var existingAccount = DataContext.Accounts.Count(a => a.Email == email);
            if (existingAccount > 0)
            {
                throw new Exception("Duplicate!");
            }

            var password = CryptoProvider.CreateNewPassword();
            var salt = CryptoProvider.GetNewSalt();
            var account = new Data.Account
            {
                Email = email,
                Name = model.Name,
                Salt = salt,
                Password = CryptoProvider.HashPassword(password, salt)
            };

            DataContext.Accounts.InsertOnSubmit(account);
            DataContext.SubmitChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}