using DbSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiftGivr.Web.Migrations
{
    public class _002_AccountsUniqueEmail : IMigration
    {
        public long Version
        {
            get { return 1; }
        }

        public string SqlCommand
        {
            get
            {
                return @"

CREATE UNIQUE NONCLUSTERED INDEX IX_Accounts_Email
	ON dbo.Accounts
	(
		Email
	)
	WITH 
	(
		STATISTICS_NORECOMPUTE = OFF
	)
	 
";
            }
        }
    }
}