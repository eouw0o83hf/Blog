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
    public class HomeController : BaseController
    {
        public HomeController(GiftGivrControllerContext context)
            : base(context) { }

        public ActionResult Index()
        {
            var allUsers = DataContext.Accounts
                                .Select(a => new { a.AccountId, a.Name })
                                .ToList()
                                .ToDictionary(a => a.AccountId, a => a.Name);
            return View(new HomeViewModel
                {
                    AllUsers = allUsers
                });
        }
    }
}