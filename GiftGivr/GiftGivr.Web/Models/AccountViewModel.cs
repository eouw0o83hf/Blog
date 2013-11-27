using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiftGivr.Web.Models
{
    public class AccountViewModel
    {
        public string Name { get; set; }
        public ICollection<GiftViewModel> Gifts { get; set; }
    }
}