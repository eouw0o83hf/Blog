﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Web.ViewModels.Blog
{
    public class Page
    {
        public string BlogName { get; set; }

        public IList<Post> Posts { get; set; }
    }
}