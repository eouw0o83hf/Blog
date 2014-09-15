using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Web.ViewModels.Users
{
    public class UserListViewModel
    {
        public ICollection<UserViewModel> LineItems { get; set; }
    }
}