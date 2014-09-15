using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Script.Serialization;

namespace Blog.Web
{
    public class BlogUser : IPrincipal, IIdentity
    {
        public int? UserId { get; set; }
        public string Upn { get; set; }
        public string Email { get; set; }

        // It's a List because JavscriptSerializer can't just figure out
        // a thing that implements ICollection. It's really not that hard.
        public List<string> Roles { get; set; }

        [ScriptIgnore]
        public IIdentity Identity
        {
            get { return this; }
        }

        public bool IsInRole(string role)
        {
            return Roles != null && Roles.Contains(role);
        }

        public string AuthenticationType
        {
            get { return "eouw0o83hf-blog-auth"; }
        }

        public bool IsAuthenticated
        {
            get { return UserId.HasValue; }
        }

        public string Name
        {
            get { return Upn; }
        }

        public static BlogUser Unauthenticated
        {
            get
            {
                return new BlogUser
                {
                    Email = null,
                    Roles = null,
                    Upn = null,
                    UserId = null
                };
            }
        }
    }
}