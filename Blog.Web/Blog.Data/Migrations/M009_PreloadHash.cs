using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbSync;

namespace Blog.Data.Migrations
{
    public class M009_PreloadHash : IMigration
    {
        public long Version { get { return 9; } }

        public string SqlCommand
        {
            get
            {
                return @"

UPDATE
	u
SET
	u.PasswordSalt = '7518B16543B536C567EACEBE6D568087398D6556E72AF00637A2F8F83153FF77',
	u.PasswordHash = '75D6FE8E9EFA75610E6DA3962B06C083968E79D4B3AD44271627599C512A8662BC04E3DB8C1E04DAF7F2234BA172513FEF5B5C0F18D03F1DF827FDB7AC70E564C6CD7B444B906C80FF85B96CFE7618DAA6849021E70F91AFBDA7C7C85C5D75417EC7B23F4C4D2493C2D843EBBA52B5FEE79463E526CD74350E37828500104790'
FROM
	dbo.Permissions p
	INNER JOIN dbo.UserPermissions up ON p.PermissionId = up.PermissionId
	INNER JOIN dbo.Users u ON up.UserId = u.UserId
WHERE
	p.Name = 'Admin'

";
            }
        }
    }
}
