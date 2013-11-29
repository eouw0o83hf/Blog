using GiftGivr.Web.Classes;
using GiftGivr.Web.Data;
using SendGrid.Transport;
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

        protected readonly CryptoProvider CryptoProvider;

        protected readonly ITransport SendGridProvider;

        protected BaseController(GiftGivrControllerContext context)
        {
            DataContext = context.DataContext;
            CryptoProvider = context.CryptoProvider;
            SendGridProvider = context.SendGridProvider;
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