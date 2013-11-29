using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiftGivr.Web.Models
{
    public class ClaimGiftViewModel
    {
        public string Gift { get; set; }
        public string GiftTarget { get; set; }
        public string GiftCreator { get; set; }

        public bool Claim { get; set; }
        public string Comment { get; set; }
    }
}