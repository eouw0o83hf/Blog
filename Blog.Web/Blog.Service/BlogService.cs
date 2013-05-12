using Blog.Data;
using Blog.Models;
using Common;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var result = (from b in BlogDb.Blogs
                          where b.UrlName.Contains(requestDomain)
                          select b.BlogId).FirstOrNull();

            if (result.HasValue)
            {
                return result.Value;
            }

            // Temp fallback
            return BlogDb.Blogs.First().BlogId;
        }

        public PaginatedList<PostModel> GetPosts(int blogId, int? page = null, int? count = null)
        {
            var query = from p in BlogDb.Posts
                        where p.BlogId == blogId
                        orderby p.PostId descending
                        select new PostModel
                        {
                            Body = p.Body,
                            CreatedDate = p.CreatedOn,
                            Identifier = p.PermalinkGuid,
                            ModifedDate = p.CreatedOn,
                            PostId = p.PostId,
                            Title = p.Title
                        };

            return new PaginatedList<PostModel>(query, page, count);
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
                Upn = dbUser.Upn
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
}
