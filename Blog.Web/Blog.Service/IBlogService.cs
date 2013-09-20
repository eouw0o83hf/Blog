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
        ICollection<BlogModel> GetBlogs();
        BlogModel GetBlog(int blogId);

        /// <summary>
        /// Gets the Posts included in the specified range
        /// </summary>
        /// <param name="blogId">Target Blog ID</param>
        /// <param name="page">0-indexed page</param>
        /// <param name="count">Defaults to 1 if left null</param>
        /// <returns></returns>
        PaginatedList<PostModel> GetPosts(int? blogId, int? page = null, int? count = null);

        PostModel GetPost(int postId);
        PostModel GetPost(Guid postIdentifier);
        
        #endregion

        #region General Blog SET

        /// <summary>
        /// Creates or updates a Post
        /// </summary>
        /// <returns>ID of inserted or updated entity</returns>
        int CreateOrUpdatePost(PostModel model);
        int CreateOrUpdateBlog(BlogModel blog);

        #endregion

        #region Authentication and Authorization

        UserModel GetOrCreateUser(IAuthenticationResponse openIdResponse);
        UserModel GetUser(int userId);

        void GrantUserPermission(int userId, PermissionEnum permission);
        Response UpdateEmail(int userId, string emailAddress, string pickupUrl);
        void SendEmailPickupInvite(int userId, string pickupUrl);
        Response AttemptEmailInvitePickup(int userId, Guid inviteId);

        #endregion
    }
}
