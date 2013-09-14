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
using Blog.Web.Filters;
using Common;
using System.Text.RegularExpressions;

namespace Blog.Web.Controllers
{
    public class AdminController : BaseController
    {
        public AdminController(BlogControllerContext context)
            : base(context) { }

        public ActionResult Index()
        {
            return View();
        }

        #region Blogs

        [HttpGet, BlogAuthorize(PermissionEnum.Admin)]
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

        [HttpGet, BlogAuthorize(PermissionEnum.Admin)]
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

        [HttpPost, BlogAuthorize(PermissionEnum.Admin)]
        public ActionResult Blog(BlogViewModel blog)
        {
            var domainModel = new BlogModel
            {
                BlogId = blog.BlogId ?? 0,
                Description = blog.Description,
                Name = blog.Name,
                UrlName = blog.UrlName
            };
            var blogId = BlogService.CreateOrUpdateBlog(domainModel);
            return RedirectToAction("Blogs");
        }

        #endregion

        #region Posts

        [HttpGet, BlogAuthorize(PermissionEnum.Admin)]
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

        [HttpGet, BlogAuthorize(PermissionEnum.Admin)]
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
                    BlogId = model.BlogId,
                    UrlTitle = model.UrlTitle
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

        [HttpPost, BlogAuthorize(PermissionEnum.Admin)]
        public ActionResult Post(EditPostViewModel model)
        {
            var postId = BlogService.CreateOrUpdatePost(new Models.PostModel
            {
                BlogId = model.BlogId.Value,
                Body = model.Body,
                Identifier = model.Identifier,
                PostId = model.PostId,
                Title = model.Title,
                UrlTitle = model.UrlTitle
            });

            return RedirectToAction("Posts");
        }

        [HttpPost, BlogAuthorize(PermissionEnum.Admin)]
        public PartialViewResult PreviewMarkdown(string markdown)
        {
            return PartialView(markdown);
        }

        #endregion

        public const string VIEWDATA_BLOGS = "VIEWDATA_BLOGS";
        protected override void CramViewData()
        {
            ViewData[VIEWDATA_BLOGS] = BlogService.GetBlogs().ToDictionary(a => a.BlogId, a => a.Name);

            base.CramViewData();
        }
    }
}
