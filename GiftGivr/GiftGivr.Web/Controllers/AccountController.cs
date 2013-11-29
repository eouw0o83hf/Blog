using GiftGivr.Web.Classes;
using GiftGivr.Web.Data;
using GiftGivr.Web.Models;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
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

            var fromUser = DataContext.Accounts.Single(a => a.AccountId == UserId.Value);

            var message = new StringBuilder()
                            .Append("<p>Hello, ")
                                .Append(model.Name)
                                .AppendLine(",</p>")
                            .Append("<p>")
                                .Append(fromUser.Name)
                                .AppendLine(" thought you should use GiftGivr! We think so too.</p>")
                            .AppendLine("<p>GiftGivr lets you and your family help each other with communicating about gifts - what you need, and what you think others should be given.</p>")
                            .Append("<p>To give it a shot, just head on over to <a href=\"http://")
                                .Append(Request.Url.Host)
                                .Append("\">")
                                .Append(Request.Url.Host)
                                .AppendLine("</a>.</p>")
                            .Append("<p>When you're asked to login, use this email address (")
                                .Append(email)
                                .Append(") and the following temporary password: <strong>")
                                .Append(password)
                                .AppendLine("</strong></p>")
                            .AppendLine("<p>If you believe you have received this email in error, please simply ignore it and we will not make any further efforts to contact you.</p>");

            var from = new MailAddress("no-reply@giftgivr.com");
            var to = new [] { new MailAddress(email, model.Name) };
            var mail = Mail.GetInstance(from, to, new MailAddress[0], new MailAddress[0], "Invitation to GiftGivr", message.ToString(), null);
            SendGridProvider.Deliver(mail);

            return RedirectToAction("Index", "Home");
        }
    }
}