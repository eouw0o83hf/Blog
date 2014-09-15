using DbSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Migrations
{
    public class M006_PublishDateForPost : IMigration
    {
        public long Version { get { return 6; } }
        public string SqlCommand
        {
            get
            {
                return @"

ALTER TABLE dbo.Posts
ADD PublishDate DATETIME NULL

";
            }
        }
    }
}
