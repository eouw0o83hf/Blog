using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Web.Controllers
{
    public class AdminController : BaseController
    {
        public AdminController(BlogControllerContext context)
            : base(context) { }
    }
}
