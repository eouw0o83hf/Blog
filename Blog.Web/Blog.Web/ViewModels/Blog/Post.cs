using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Web.ViewModels.Blog
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PostDate { get; set; }
        public string RawBody { get; set; }

        public Guid Identifier { get; set; }
        public string UrlTitle { get; set; }
    }
}