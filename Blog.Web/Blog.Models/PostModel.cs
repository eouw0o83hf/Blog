using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class PostModel
    {
        public int? PostId { get; set; }
        public Guid Identifier { get; set; }

        public int BlogId { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifedDate { get; set; }
        public DateTime? PublishDate { get; set; }

        public bool IsDraft { get; set; }

        public string Title { get; set; }
        public string UrlTitle { get; set; }
        public string Body { get; set; }
    }
}
