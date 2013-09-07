using Blog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Web.Controllers
{
    public class BlogControllerContext
    {
        public IBlogService BlogService { get; set; }

        public string SendGridSmtpServer { get; set; }
        public string SendGridUsername { get; set; }
        public string SendGridPassword { get; set; }

        public string CdnAccountName { get; set; }
        public string CdnAccessKey { get; set; }
    }
}