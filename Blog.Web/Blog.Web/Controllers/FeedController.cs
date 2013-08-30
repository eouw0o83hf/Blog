using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Web.ViewModels.Feed;
using Common;
using System.Configuration;

namespace Blog.Web.Controllers
{
    public class FeedController : BaseController
    {
        public FeedController(BlogControllerContext context)
            : base(context) { }

        [HttpGet]
        public ActionResult Index(string blog)
        {
            int? blogId = null;
            if (blog.IsBlank())
            {
                blogId = BlogService.GetBlogId(blog);
            }

            if (blogId == null)
            {
                blog = ConfigurationManager.AppSettings["DefaultBlogName"];
                blogId = BlogService.GetBlogId(blog);
            }

            if (blogId == null)
            {
                throw new HttpException(404, "Blog not found");
            }

            var timeZone = GetLocalTime();
            var posts = BlogService.GetPosts(blogId.Value, 0, 1000000);
            var postVms = posts.Select(a => new Post
            {
                Id = a.PostId.Value,
                PostDate = TimeZoneInfo.ConvertTime(a.CreatedDate, TimeZoneInfo.Utc, timeZone),
                RawBody = a.Body,
                Title = a.Title,
                PostIdentifier = a.Identifier,
                UrlTitle = a.UrlTitle
            });

            var blogModel = BlogService.GetBlog(blogId.Value);

            var result = new FeedViewModel
            {
                BlogName = blogModel.Name,
                UrlName = blogModel.UrlName,
                Posts = postVms.ToList()
            };

            Response.ContentType = "text/xml";

            return View(result);
        }

    }
}
