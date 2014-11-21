using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbSync;

namespace Blog.Data.Migrations
{
    public class M008_SimpleAuthentication : IMigration
    {
        public long Version { get { return 8; } }

        public string SqlCommand
        {
            get
            {
                return @"

ALTER TABLE dbo.Users
ADD PasswordSalt NVARCHAR(MAX) NULL,
	PasswordHash NVARCHAR(MAX) NULL

";
            }
        }
    }
}
