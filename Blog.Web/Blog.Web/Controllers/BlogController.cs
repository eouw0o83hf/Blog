using Blog.Service;
using Blog.Web.ViewModels.Blog;
using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Web.Controllers
{
    public class BlogController : BaseController
    {
        public BlogController(BlogControllerContext context)
            : base(context) { }
        
        [HttpGet]
        public ActionResult Page(string blog)
        {
            var blogId = ConfigurationManager.AppSettings["DefaultBlogId"].TryParseInt();
            if (blog.IsNotBlank())
            {
                blogId = BlogService.GetBlogId(blog);
                if (!blogId.HasValue)
                {
                    throw new HttpException(404, "Blog not found");
                }
            }

            var timeZone = GetLocalTime();
            var posts = BlogService.GetPosts(blogId.Value, 0, 1000000);
            var postVms = posts.Select(a => new Post
            {
                Id = a.PostId.Value,
                PostDate = TimeZoneInfo.ConvertTime(a.CreatedDate, TimeZoneInfo.Utc, timeZone),
                RawBody = a.Body,
                Title = a.Title
            });

            var blogModel = BlogService.GetBlog(blogId.Value);

            var result = new Page
            {
                BlogName = blogModel.Name,
                Posts = postVms.ToList()
            };

            return View(result);
        }
    }
}
