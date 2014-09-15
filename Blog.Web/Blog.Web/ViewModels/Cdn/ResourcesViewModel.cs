using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Web.ViewModels.Cdn
{
    public class ResourcesViewModel
    {
        public string Directory { get; set; }
        public IEnumerable<ResourceLineItem> LineItems { get; set; }
    }

    public class ResourceLineItem
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}