using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Web.ViewModels.Blog
{
    public class Page
    {
        public string BlogName { get; set; }
        public string UrlName { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}