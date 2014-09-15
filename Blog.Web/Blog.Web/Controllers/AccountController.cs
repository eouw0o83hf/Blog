using Blog.Web.ViewModels.Account;
using Blog.Web.ViewModels.Feed;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Blog.Web.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(BlogControllerContext context)
            : base(context) { }

        [HttpGet, Authorize]
        public ActionResult Index()
        {
            if (!(User is BlogUser))
            {
                throw new HttpException(401, "Unauthorized");
            }

            var user = BlogService.GetUser(((BlogUser)User).UserId ?? 0);
            var viewModel = new AccountViewModel
            {
                EmailAddress = user.Email,
                EmailIsVerified = user.EmailIsVerified
            };

            return View(viewModel);
        }

        protected string TemplatePickupUrl
        {
            get
            {
                return Url.RouteUrl(RouteNames.ConfirmEmail, new { id = Guid.Empty }, "https");
            }
        }

        [HttpPost, Authorize]
        public ActionResult UpdateEmail(AccountViewModel model)
        {            
            var response = BlogService.UpdateEmail(((BlogUser)User).UserId.Value, model.EmailAddress, TemplatePickupUrl);

            Notification notification;
            if (response.Success)
            {
                notification = new Notification
                {
                    Type = NotificationType.Information,
                    Subject = "Email Confirmation Required",
                    Message = "In order to confirm your email address, a verification email has been sent to the address you provided."
                };
            }
            else
            {
                notification = new Notification
                {
                    Type = NotificationType.Error,
                    Subject = "Update Failed",
                    Message = "Your email address could not be updated; " + response.Message
                };
            }

            TempData.StoreNotification(notification);

            return RedirectToAction("Index");
        }

        [HttpGet, Authorize]
        public ActionResult RequestEmailLink()
        {
            BlogService.SendEmailPickupInvite(((BlogUser)User).UserId.Value, TemplatePickupUrl);
            TempData.StoreNotification(new Notification
            {
                Type = NotificationType.Confirmation,
                Subject = "Confirmation Email Sent",
                Message = "A confirmation email has been sent to the address you have on file with us."
            });
            return RedirectToAction("Index");
        }

        [HttpGet, Authorize]
        public ActionResult VerifyEmail(Guid id)
        {
            var response = BlogService.AttemptEmailInvitePickup(((BlogUser)User).UserId.Value, id);
            
            Notification notification;
            if (response.Success)
            {
                notification = new Notification
                {
                    Type = NotificationType.Confirmation,
                    Subject = "Email Verified",
                    Message = "Your email address has been verified. Thanks!"
                };
            }
            else
            {
                notification = new Notification
                {
                    Type = NotificationType.Error,
                    Subject = "Verification Failed",
                    Message = "Your email address could not be verified. " + response.Message
                };
            }
            TempData.StoreNotification(notification);

            return RedirectToAction("Index");
        }

        public void SendVerificationEmail()
        {
            //var from = new MailAddress("noreply@eouw0o83hf.com");
            //var to = new[] { new MailAddress("nathanlandis@gmail.com") };
            //var subject = "Testing SendGrid!";
            //var html = @"<h1>Oh hai!</h1><p>Here's where the body would be, along with a</p><h2><a href=""#"">Confirmation Link</a></h2>";

            //var message = SendGrid.Mail.GetInstance(from, to, new MailAddress[0], new MailAddress[0], subject, html, null);
            //var creds = new NetworkCredential(Context.SendGridUsername, Context.SendGridPassword);
            //var transport = SendGrid.Transport.SMTP.GetInstance(creds);
            //transport.Deliver(message);
        }
    }
}
