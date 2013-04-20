using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data
{
    public class BlogDataContextWrapper : BlogDataContext
    {
        public BlogDataContextWrapper(string connectionString)
            : base(connectionString) { }
    }
}
