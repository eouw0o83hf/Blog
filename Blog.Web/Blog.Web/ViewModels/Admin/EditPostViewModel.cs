﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Web.ViewModels.Admin
{
    public class EditPostViewModel
    {
        public int? PostId { get; set; }
        public Guid Identifier { get; set; }

        public int? BlogId { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifedDate { get; set; }

        [AllowHtml]
        public string Title { get; set; }
        [AllowHtml]
        public string Body { get; set; }
    }
}