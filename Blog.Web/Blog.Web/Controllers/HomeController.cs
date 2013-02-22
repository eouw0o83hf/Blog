using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Web.ViewModels.Home;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Main()
        {
            return View(new MainViewModel());
        }
    }
}
