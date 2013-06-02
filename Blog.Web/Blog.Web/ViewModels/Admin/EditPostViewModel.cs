using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Web.ViewModels.Admin
{
    public class EditPostViewModel
    {
        public int? PostId { get; set; }
        public Guid Identifier { get; set; }

        public int? BlogId { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifedDate { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }
    }
}