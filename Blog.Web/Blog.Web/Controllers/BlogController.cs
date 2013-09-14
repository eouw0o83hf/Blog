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

            if(blogId == null)
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
                Identifier = a.Identifier,
                UrlTitle = a.UrlTitle
            });

            var blogModel = BlogService.GetBlog(blogId.Value);

            var result = new Page
            {
                BlogName = blogModel.Name,
                UrlName = blogModel.UrlName,
                Posts = postVms.ToList()
            };

            return View(result);
        }

        [HttpGet]
        public ActionResult Permalink(Guid postIdentifier, string urlTitle)
        {
            var post = BlogService.GetPost(postIdentifier);
            if (post == null)
            {
                throw new HttpException(404, "Post not found");
            }

            var viewModel = new Post
            {
                Id = post.PostId.Value,
                Identifier = post.Identifier,
                PostDate = post.CreatedDate,
                RawBody = post.Body,
                Title = post.Title,
                UrlTitle = post.UrlTitle
            };

            return View(new Permalink
                {
                    Post = viewModel
                });
        }
    }
}
