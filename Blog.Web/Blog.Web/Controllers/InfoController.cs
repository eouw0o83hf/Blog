using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Web.Controllers
{
    public class InfoController : BaseController
    {
        public InfoController(BlogControllerContext context)
            : base(context) { }

        [HttpGet]
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult VersionInfo()
        {
            return View();
        }
    }
}
