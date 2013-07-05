using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Web.ViewModels.Account
{
    public class BlobUploadResultViewModel
    {
        public string UploadedUrl { get; set; }
        public IEnumerable<string> AllBlobs { get; set; }
    }
}