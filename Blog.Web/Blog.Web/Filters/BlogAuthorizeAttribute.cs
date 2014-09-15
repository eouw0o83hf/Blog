using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class BlogAuthorizeAttribute : AuthorizeAttribute
    {
        public BlogAuthorizeAttribute()
            : base() { }

        public BlogAuthorizeAttribute(PermissionEnum permission)
            : base()
        {
            Roles = permission.ToString();
        }
    }
}