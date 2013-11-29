using GiftGivr.Web.Data;
using SimpleCrypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiftGivr.Web.Classes
{
    public class GiftGivrControllerContext
    {
        public GiftGivrDataContext DataContext { get; set; }

        public CryptoProvider CryptoProvider { get; set; }

        public string SendGridSmtpServer { get; set; }
        public string SendGridUsername { get; set; }
        public string SendGridPassword { get; set; }
    }
}