using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Web.ViewModels.Account
{
    public class AccountViewModel
    {
        public string EmailAddress { get; set; }
        public bool EmailIsVerified { get; set; }
    }
}