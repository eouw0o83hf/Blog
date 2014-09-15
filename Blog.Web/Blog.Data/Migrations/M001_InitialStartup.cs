using DbSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Migrations
{
    public class M001_InitialStartup : IMigration
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

CREATE TABLE dbo.Blogs
(
	BlogId INT NOT NULL IDENTITY(1, 1),
	UrlName NVARCHAR(MAX) NOT NULL,
	DisplayName NVARCHAR(MAX) NOT NULL,
	Description NVARCHAR(MAX) NOT NULL,
	CONSTRAINT PK_Blogs PRIMARY KEY(BlogId)
)

CREATE TABLE dbo.Posts
(
	PostId INT NOT NULL IDENTITY(1, 1),
	BlogId INT NOT NULL,
	PermalinkGuid UNIQUEIDENTIFIER NOT NULL DEFAULT(NEWID()),
	UrlTitle NVARCHAR(MAX) NOT NULL,
	Title NVARCHAR(MAX) NOT NULL,
	Body NVARCHAR(MAX) NOT NULL,
	CreatedOn DATETIME NOT NULL DEFAULT(GETUTCDATE()),
	CreatedBy NVARCHAR(MAX) NOT NULL
	CONSTRAINT PK_Posts PRIMARY KEY(PostId)
	CONSTRAINT FK_Posts_Blogs FOREIGN KEY(BlogId) REFERENCES Blogs(BlogId)
)

";
            }
        }
    }
}
