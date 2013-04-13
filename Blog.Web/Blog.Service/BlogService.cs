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

        public PaginatedList<PostModel> GetPosts(int? page, int? count)
        {
            return null;
        }
    }
}
