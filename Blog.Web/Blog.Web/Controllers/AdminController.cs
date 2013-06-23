using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Web.ViewModels.Admin;
using Blog.Models;

namespace Blog.Web.Controllers
{
    public class AdminController : BaseController
    {
        public AdminController(BlogControllerContext context)
            : base(context) { }

        #region Blogs

        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult Blogs()
        {
            var blogs = BlogService.GetBlogs();
            var result = blogs.Select(a => new BlogViewModel
            {
                BlogId = a.BlogId,
                Description = a.Description,
                Name = a.Name,
                UrlName = a.UrlName
            });
            return View(result);
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult Blog(int? blogId)
        {
            BlogViewModel viewModel;
            if (blogId.HasValue)
            {
                var blog = BlogService.GetBlog(blogId.Value);
                viewModel = new BlogViewModel
                {
                    BlogId = blog.BlogId,
                    Description = blog.Description,
                    Name = blog.Name,
                    UrlName = blog.UrlName
                };
            }
            else
            {
                viewModel = new BlogViewModel();
            }
            return View(viewModel);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public ActionResult Blog(BlogViewModel blog)
        {
            var domainModel = new BlogModel
            {
                BlogId = blog.BlogId,
                Description = blog.Description,
                Name = blog.Name,
                UrlName = blog.UrlName
            };
            var blogId = BlogService.CreateOrUpdateBlog(domainModel);
            return RedirectToAction("Blogs");
        }

        #endregion

        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult Posts()
        {
            var posts = BlogService.GetPosts(null, 0, int.MaxValue);

            var result = new PostListViewModel
            {
                Items = posts.Select(a => new PostListLineItem
                {
                    Created = a.CreatedDate,
                    PostId = a.PostId.Value,
                    Title = a.Title
                }).ToList()
            };

            return View(result);
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult Post(int? postId)
        {
            EditPostViewModel viewModel;
            if (postId.HasValue)
            {
                var model = BlogService.GetPost(postId.Value);
                viewModel = new EditPostViewModel
                {
                    Body = model.Body,
                    CreatedDate = model.CreatedDate,
                    Identifier = model.Identifier,
                    ModifedDate = model.ModifedDate,
                    PostId = model.PostId,
                    Title = model.Title,
                    BlogId = model.BlogId
                };
            }
            else
            {
                viewModel = new EditPostViewModel
                {
                    PostId = null,
                    Identifier = Guid.NewGuid(),
                    BlogId = null
                };
            }

            return View(viewModel);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public ActionResult Post(EditPostViewModel model)
        {
            var postId = BlogService.CreateOrUpdatePost(new Models.PostModel
            {
                BlogId = model.BlogId.Value,
                Body = model.Body,
                Identifier = model.Identifier,
                PostId = model.PostId,
                Title = model.Title,
                UrlTitle = string.Empty
            });

            return RedirectToAction("Posts");
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public PartialViewResult PreviewMarkdown(string markdown)
        {
            return PartialView(markdown);
        }

        public const string VIEWDATA_BLOGS = "VIEWDATA_BLOGS";
        protected override void CramViewData()
        {
            ViewData[VIEWDATA_BLOGS] = BlogService.GetBlogs().ToDictionary(a => a.BlogId, a => a.Name);

            base.CramViewData();
        }
    }
}
