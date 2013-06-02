using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Upn { get; set; }
        public string Email { get; set; }
        public string Handle { get; set; }

        public ICollection<PermissionEnum> Permissions { get; set; }
    }
}
