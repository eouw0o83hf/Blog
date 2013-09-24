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
using Blog.Web.ViewModels.Users;

namespace Blog.Web.Controllers
{
    public class UsersController : BaseController
    {
        public UsersController(BlogControllerContext context)
            : base(context) { }

        [HttpGet, BlogAuthorize(PermissionEnum.Admin)]
        public ActionResult List()
        {
            var allUsers = BlogService.GetAllUsers();
            var result = new UserListViewModel
            {
                LineItems = (from a in allUsers
                             orderby 
                                a.Permissions.Count descending,
                                a.Email.IsBlank() descending,
                                a.EmailIsVerified descending
                             select new UserViewModel
                             {
                                 Email = a.Email,
                                 EmailConfirmed = a.EmailIsVerified,
                                 Permissions = a.Permissions.Select(b => new PermissionViewModel { PermissionType = b, IsInRole = true }).ToList(),
                                 UserId = a.UserId,
                                 Upn = a.Upn
                             }).ToList()
            };
            return View(result);
        }

        [HttpGet, BlogAuthorize(PermissionEnum.Admin)]
        public ActionResult User(int userId)
        {
            var user = BlogService.GetUser(userId);
            var result = new UserViewModel
            {
                Email = user.Email,
                EmailConfirmed = user.EmailIsVerified,
                Permissions = user.Permissions.Select(b => new PermissionViewModel { PermissionType = b, IsInRole = true }).ToList(),
                UserId = user.UserId,
                Upn = user.Upn
            };
            return View(result);
        }

        [HttpPost, BlogAuthorize(PermissionEnum.Admin)]
        public ActionResult User(UserViewModel model)
        {
            return RedirectToAction("Index");
        }

        public const string VIEWDATA_PERMISSIONS = "VIEWDATA_PERMISSIONS";
        protected override void CramViewData()
        {
            base.CramViewData();

            ViewData[VIEWDATA_PERMISSIONS] = Enum.GetValues(typeof(PermissionEnum)).Cast<PermissionEnum>().ToList();
        }
    }
}
