﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Blog.Web.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(BlogControllerContext context)
            : base(context) { }

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
