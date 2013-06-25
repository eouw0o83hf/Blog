using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Web.ViewModels.Admin
{
    public class BlogViewModel
    {
        public int? BlogId { get; set; }
        public string Name { get; set; }
        public string UrlName { get; set; }
        public string Description { get; set; }
    }
}