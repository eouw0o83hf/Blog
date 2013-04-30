using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Web.ViewModels.Authentication
{
    public class LoginViewModel
    {
        public ICollection<OpenIdProvider> OpenIdProviders { get; set; }
    }

    public class OpenIdProvider
    {
        public string Name { get; set; }
        public string LoginUrl { get; set; }
    }
}