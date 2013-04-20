using Blog.Data;
using Blog.Models;
using Common;
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
    }
}
