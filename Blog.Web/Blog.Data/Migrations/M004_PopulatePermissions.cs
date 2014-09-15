using DbSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Migrations
{
    public class M004_PopulatePermissions : IMigration
    {
        public long Version
        {
            get { return 4; }
        }

        public string SqlCommand
        {
            get 
            {
                return @"

INSERT INTO Permissions
SELECT 1, 'Admin'

";
            }
        }
    }
}
