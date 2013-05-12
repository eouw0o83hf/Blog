using Blog.Models;
using Common;
using DotNetOpenAuth.OpenId.RelyingParty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service
{
    public interface IBlogService
    {
        #region General Blog GET

        int? GetBlogId(string requestDomain);
        PaginatedList<PostModel> GetPosts(int blogId, int? page = null, int? count = null);

        #endregion

        #region Authentication and Authorization

        UserModel GetOrCreateUser(IAuthenticationResponse openIdResponse);

        #endregion
    }
}
