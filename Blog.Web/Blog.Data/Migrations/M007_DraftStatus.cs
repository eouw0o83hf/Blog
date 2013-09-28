using DbSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Migrations
{
    public class M007_DraftStatus : IMigration
    {
        public long Version { get { return 7; } }
        public string SqlCommand
        {
            get
            {
                return @"

ALTER TABLE dbo.Posts
ADD IsDraft BIT NOT NULL DEFAULT(0)

";
            }
        }
    }
}
