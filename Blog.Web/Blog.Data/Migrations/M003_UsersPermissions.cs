using DbSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Migrations
{
    public class M003_UsersPermissions : IMigration
    {
        public long Version
        {
            get { return 3; }
        }

        public string SqlCommand
        {
            get 
            {
                return @"
CREATE TABLE IdentityProviders
(
	IdentityProviderId INT NOT NULL IDENTITY(1, 1),
	Uri NVARCHAR(MAX) NOT NULL,
	CONSTRAINT PK_IdentityProviders PRIMARY KEY(IdentityProviderId)
)

CREATE TABLE Users
(
	UserId INT NOT NULL IDENTITY(1, 1),
	IdentityProviderId INT NOT NULL,
	Upn NVARCHAR(200) NOT NULL,
	Email NVARCHAR(MAX) NULL,
	Handle NVARCHAR(MAX) NULL,
	TimeZoneOffset DATETIMEOFFSET NULL,
	CONSTRAINT PK_Users PRIMARY KEY(UserId),
	CONSTRAINT IX_Users UNIQUE (IdentityProviderId, Upn),
	CONSTRAINT FK_Users_IdentityProviders FOREIGN KEY(IdentityProviderId) REFERENCES IdentityProviders(IdentityProviderId)
)

CREATE TABLE Permissions
(
	PermissionId INT NOT NULL,
	Name NVARCHAR(MAX) NOT NULL,
	CONSTRAINT PK_Permissions PRIMARY KEY(PermissionId)
)

CREATE TABLE UserPermissions
(
	UserId INT NOT NULL,
	PermissionId INT NOT NULL,
	CONSTRAINT PK_UserPermissions PRIMARY KEY(UserId, PermissionId),
	CONSTRAINT FK_UserPermissions_Users FOREIGN KEY(UserId) REFERENCES Users(UserId),
	CONSTRAINT FK_UserPermissions_Permissiosn FOREIGN KEY(PermissionId) REFERENCES Permissions(PermissionId)
)
";
            }
        }
    }
}
