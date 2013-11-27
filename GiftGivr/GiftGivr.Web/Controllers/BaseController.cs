using GiftGivr.Web.Classes;
using GiftGivr.Web.Data;
using SimpleCrypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GiftGivr.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly GiftGivrDataContext DataContext;

        protected readonly ICryptoService CryptoService;

        protected readonly string SendGridSmtpServer;
        protected readonly string SendGridUsername;
        protected readonly string SendGridPassword;

        protected BaseController(GiftGivrControllerContext context)
        {
            DataContext = context.DataContext;

            CryptoService = context.CryptoService;

            SendGridSmtpServer = context.SendGridSmtpServer;
            SendGridUsername = context.SendGridUsername;
            SendGridPassword = context.SendGridPassword;
        }

        protected int? UserId
        {
            get
            {
                return User.Identity.IsAuthenticated ? int.Parse(User.Identity.Name) : (int?)null;
            }
        }
    }
}