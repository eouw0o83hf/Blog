using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Web.ViewModels.Users
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Upn { get; set; }
        
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

        public List<PermissionViewModel> Permissions { get; set; }
    }

    public class PermissionViewModel
    {
        public PermissionEnum PermissionType { get; set; }
        public bool IsInRole { get; set; }
    }
}