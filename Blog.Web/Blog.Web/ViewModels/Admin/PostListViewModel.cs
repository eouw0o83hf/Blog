using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Web.ViewModels.Admin
{
    public class PostListViewModel
    {
        public int BlogId { get; set; }
        public ICollection<PostListLineItem> Items { get; set; }
    }

    public class PostListLineItem
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
    }
}