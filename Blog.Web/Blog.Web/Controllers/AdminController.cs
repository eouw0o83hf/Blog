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

namespace Blog.Web.Controllers
{
    public class AdminController : BaseController
    {
        public AdminController(BlogControllerContext context)
            : base(context) { }
        
        [HttpGet]
        [Authorize]
        public ActionResult Posts()
        {
            var blogId = BlogService.GetBlogId(Request.Url.Host);
            var posts = BlogService.GetPosts(blogId.Value, 0, int.MaxValue);

            var result = new PostListViewModel
            {
                BlogId = blogId.Value,
                Items = posts.Select(a => new PostListLineItem
                {
                    Created = a.CreatedDate,
                    PostId = a.PostId.Value,
                    Title = a.Title
                }).ToList()
            };

            return View(result);
        }

        [HttpGet]
        [Authorize]
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
                    BlogId = GetBlogId()
                };
            }

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Post(EditPostViewModel model)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Authorize]
        public PartialViewResult PreviewMarkdown(string markdown)
        {
            return PartialView(markdown);
        }
    }
}
