using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiftGivr.Web.Models
{
    public class AddCommentViewModel
    {
        public string Gift { get; set; }
        public string GiftCreator { get; set; }
        public string Comment { get; set; }
    }
}