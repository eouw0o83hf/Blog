using Blog.Data;
using Blog.Models;
using Common;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blog.Service
{
    public class BlogService : IBlogService
    {
        protected readonly BlogDataContext BlogDb;

        public BlogService(BlogDataContext blogDb)
        {
            BlogDb = blogDb;
        }
        
        public int? GetBlogId(string requestDomain)
        {
            return (from b in BlogDb.Blogs
                    where b.UrlName == requestDomain
                    select b.BlogId).FirstOrNull();
        }

        public ICollection<BlogModel> GetBlogs()
        {
            return BlogDb.Blogs.Select(a => a.ToModel()).ToList();
        }

        public BlogModel GetBlog(int blogId)
        {
            return BlogDb.Blogs.SelectFirstOrDefault(a => a.BlogId == blogId, a => a.ToModel());
        }

        public int CreateOrUpdateBlog(BlogModel blog)
        {
            var dbBlog = BlogDb.Blogs.FirstOrDefault(a => a.BlogId == blog.BlogId);
            if (dbBlog == null)
            {
                dbBlog = new Data.Blog();
                BlogDb.Blogs.InsertOnSubmit(dbBlog);
            }

            dbBlog.DisplayName = blog.Name;
            dbBlog.UrlName = blog.UrlName;
            dbBlog.Description = blog.Description;

            BlogDb.SubmitChanges();

            blog.BlogId = dbBlog.BlogId;
            return blog.BlogId;
        }

        public PaginatedList<PostModel> GetPosts(int? blogId, int? page = null, int? count = null)
        {
            var query = from p in BlogDb.Posts
                        where blogId == null || p.BlogId == blogId
                        orderby p.PostId descending
                        select p.ToModel();

            return new PaginatedList<PostModel>(query, page, count);
        }

        public PostModel GetPost(int postId)
        {
            return BlogDb.Posts.SelectFirstOrDefault(a => a.PostId == postId, a => a.ToModel());
        }

        public PostModel GetPost(Guid postIdentifier)
        {
            return BlogDb.Posts.SelectFirstOrDefault(a => a.PermalinkGuid == postIdentifier, a => a.ToModel());
        }

        public int CreateOrUpdatePost(PostModel model)
        {
            Post dbPost;
            if (model.PostId.HasValue)
            {
                dbPost = BlogDb.Posts.Single(a => a.PostId == model.PostId.Value);
            }
            else
            {
                dbPost = new Post
                {
                    PermalinkGuid = model.Identifier,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = System.Threading.Thread.CurrentPrincipal.Identity.Name
                };
                BlogDb.Posts.InsertOnSubmit(dbPost);
            }

            dbPost.BlogId = model.BlogId;
            dbPost.Body = model.Body;
            dbPost.Title = model.Title;
            dbPost.UrlTitle = model.UrlTitle ?? Regex.Replace(model.Title, @"[^A-Za-z0-9_\.~]+", "-");

            BlogDb.SubmitChanges();

            model.PostId = dbPost.PostId;

            return dbPost.PostId;
        }

        public UserModel GetOrCreateUser(IAuthenticationResponse openIdResponse)
        {
            var ipUriString = openIdResponse.Provider.Uri.ToString();
            var upn = openIdResponse.ClaimedIdentifier.ToString();

            var dbUser = (from i in BlogDb.IdentityProviders
                          from u in i.Users
                          where i.Uri == ipUriString
                            && u.Upn == upn
                          select u).FirstOrDefault();

            if (dbUser == null)
            {
                var identityProvider = getIdentityProvider(ipUriString);
                if (identityProvider == null)
                {
                    identityProvider = createIdentityProvider(ipUriString);
                }

                var details = openIdResponse.GetExtension<ClaimsResponse>();
                string nickname = null, email = null;
                if (details != null)
                {
                    nickname = details.Nickname;
                    email = details.Email;
                    if (string.IsNullOrEmpty(nickname))
                    {
                        nickname = openIdResponse.FriendlyIdentifierForDisplay;
                    }
                }
                dbUser = createUser(identityProvider.IdentityProviderId, upn, email, nickname);
            }

            return new UserModel
            {
                UserId = dbUser.UserId,
                Email = dbUser.Email,
                Handle = dbUser.Handle,
                Upn = dbUser.Upn,
                Permissions = dbUser.UserPermissions.Select(a => (PermissionEnum)a.PermissionId).ToList()
            };
        }

        #region Repository

        protected IdentityProvider getIdentityProvider(string uri)
        {
            return BlogDb.IdentityProviders.FirstOrDefault(a => a.Uri == uri);
        }

        protected IdentityProvider createIdentityProvider(string uri)
        {
            var dbIdentityProvider = getIdentityProvider(uri);
            
            if (dbIdentityProvider != null)
            {
                throw new ArgumentException("Identity Provider already exists for URI " + uri);
            }

            dbIdentityProvider = new IdentityProvider
            {
                Uri = uri
            };
            BlogDb.IdentityProviders.InsertOnSubmit(dbIdentityProvider);
            BlogDb.SubmitChanges();
            
            return dbIdentityProvider;
        }

        protected User createUser(int identityProviderId, string upn, string email, string handle)
        {
            var dbUser = BlogDb.Users.FirstOrDefault(a => a.IdentityProviderId == identityProviderId && a.Upn == upn);
            if (dbUser != null)
            {
                throw new Exception(string.Format("User already exists for Identity Provider {0}, UPN {1}", identityProviderId, upn));
            }

            dbUser = new User
            {
                IdentityProviderId = identityProviderId,
                Upn = upn,
                Email = email,
                Handle = handle
            };
            BlogDb.Users.InsertOnSubmit(dbUser);
            BlogDb.SubmitChanges();

            return dbUser;
        }

        #endregion
    }

    public static class Extensions
    {
        public static PostModel ToModel(this Post post)
        {
            return new PostModel
            {
                Body = post.Body,
                CreatedDate = post.CreatedOn,
                Identifier = post.PermalinkGuid,
                ModifedDate = post.CreatedOn,
                PostId = post.PostId,
                Title = post.Title,
                BlogId = post.BlogId
            };
        }

        public static BlogModel ToModel(this Blog.Data.Blog blog)
        {
            return new BlogModel
            {
                BlogId = blog.BlogId,
                Description = blog.Description,
                Name = blog.DisplayName,
                UrlName = blog.UrlName
            };
        }
    }
}
