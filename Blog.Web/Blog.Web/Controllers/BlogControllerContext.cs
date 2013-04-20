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
    }
}