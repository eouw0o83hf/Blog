using GiftGivr.Web.Classes;
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
            return View();
        }
    }
}