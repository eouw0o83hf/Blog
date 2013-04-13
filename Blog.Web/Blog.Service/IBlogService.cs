using Blog.Models;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service
{
    public interface IBlogService
    {
        PaginatedList<PostModel> GetPosts(int? page, int? count);
    }
}
