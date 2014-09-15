using DbSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Migrations
{
    public class M005_EmailInvites : IMigration
    {
        public long Version { get { return 5; } }
        public string SqlCommand
        {
            get
            {
                return @"

ALTER TABLE dbo.Users
ADD EmailIsVerified BIT NULL

CREATE TABLE dbo.EmailVerifications
(
	Id UNIQUEIDENTIFIER NOT NULL,
	UserId INT NOT NULL,
	Created DATETIME NOT NULL,
	Expires DATETIME NOT NULL,
	CONSTRAINT PK_EmailVerifications PRIMARY KEY(Id),
	CONSTRAINT FK_EmailVerifications_Users
		FOREIGN KEY(UserId)
		REFERENCES Users(UserId)
)

";
            }
        }
    }
}
