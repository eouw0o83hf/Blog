using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiftGivr.Web.Models
{
    public class GiftCollectionViewModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public bool ThisIsYou { get; set; }

        public ICollection<GiftViewModel> Gifts { get; set; }
    }
}