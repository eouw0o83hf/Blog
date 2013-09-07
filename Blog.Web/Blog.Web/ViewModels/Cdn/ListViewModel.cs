using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Web.ViewModels.Cdn
{
    public class ListViewModel
    {
        public ICollection<ContainerLineItem> Containers { get; set; }
    }

    public class ContainerLineItem
    {
        public string Name { get; set; }
        public string Uri { get; set; }
    }
}