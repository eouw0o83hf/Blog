using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Web
{
    public class RequireHttpsNonDebugAttribute : RequireHttpsAttribute
    {
        protected override void HandleNonHttpsRequest(AuthorizationContext filterContext)
        {
            if (!Debugger.IsAttached)
            {
                base.HandleNonHttpsRequest(filterContext);
            }
        }
    }
}