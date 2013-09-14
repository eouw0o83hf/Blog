using Blog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service
{
    public class BlogServiceContext
    {
        public BlogDataContext BlogDb { get; set; }

        public string SendGridSmtpServer { get; set; }
        public string SendGridUsername { get; set; }
        public string SendGridPassword { get; set; }
    }
}
