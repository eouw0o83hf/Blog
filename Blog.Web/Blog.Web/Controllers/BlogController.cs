using Blog.Service;
using Blog.Web.ViewModels.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Web.Controllers
{
    public class BlogController : BaseController
    {
        public BlogController(BlogControllerContext context)
            : base(context) { }

        protected virtual int? GetBlogId()
        {
            return BlogService.GetBlogId(Request.Url.Host);
        }

        protected virtual TimeZoneInfo GetLocalTime()
        {
            return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
        }

        [HttpGet]
        public ActionResult Page()
        {
            var blogId = GetBlogId();
            if (!blogId.HasValue)
            {
                throw new HttpException(404, "Blog not found");
            }

            var timeZone = GetLocalTime();
            var posts = BlogService.GetPosts(blogId.Value, 0, 1000000);
            var postVms = posts.Select(a => new Post
            {
                Id = a.PostId,
                PostDate = TimeZoneInfo.ConvertTime(a.CreatedDate, TimeZoneInfo.Utc, timeZone),
                RawBody = a.Body,
                Title = a.Title
            });

            var result = new Page
            {
                BlogName = "eouw0o83hf.com Blog",
                Posts = postVms.ToList()
            };

            return View(result);
        }
    }
}
