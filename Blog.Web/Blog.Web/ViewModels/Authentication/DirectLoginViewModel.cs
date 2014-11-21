using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Web.ViewModels.Authentication
{
    public class DirectLoginViewModel
    {
        public string Upn { get; set; }
        public string Password { get; set; }
    }
}