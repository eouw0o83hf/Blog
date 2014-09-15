using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Web.ViewModels.Feed
{
    public class FeedViewModel
    {
        public string BlogName { get; set; }
        public string UrlName { get; set; }

        public ICollection<Post> Posts { get; set; }
    }

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PostDate { get; set; }
        public string RawBody { get; set; }

        public Guid PostIdentifier { get; set; }
        public string UrlTitle { get; set; }
    }
}