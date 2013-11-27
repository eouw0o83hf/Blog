using DbSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiftGivr.Web.Migrations
{
    public class _001_InitialDbCreate : IMigration
    {
        public long Version
        {
            get { return 0; }
        }

        public string SqlCommand
        {
            get
            {
                return @"

CREATE TABLE dbo.Accounts
(
	AccountId INT NOT NULL IDENTITY(1, 1)
		CONSTRAINT PK_Accounts PRIMARY KEY,
	Email NVARCHAR(200),
	Salt NVARCHAR(MAX),
	Password NVARCHAR(MAX),
	Name NVARCHAR(MAX)
)

CREATE TABLE dbo.Gifts
(
	GiftId INT NOT NULL IDENTITY(1, 1)
		CONSTRAINT PK_Gifts PRIMARY KEY,
	TargetAccountId INT NOT NULL,
	CreatorAccountId INT NOT NULL,
	ClaimedByAccountId INT NULL,
	Name NVARCHAR(MAX) NOT NULL,
	PurchaseUrl NVARCHAR(MAX) NULL,
	Description TEXT NULL,
	CONSTRAINT FK_Gifts_Accounts_Target
		FOREIGN KEY(TargetAccountId) REFERENCES dbo.Accounts(AccountId),
	CONSTRAINT FK_Gifts_Accounts_Creator
		FOREIGN KEY(CreatorAccountId) REFERENCES dbo.Accounts(AccountId),
	CONSTRAINT FK_Gifts_Accounts_Claimed
		FOREIGN KEY(ClaimedByAccountId) REFERENCES dbo.Accounts(AccountId)
)

CREATE TABLE dbo.GiftQuestions
(
	GiftQuestionId INT NOT NULL IDENTITY(1, 1)
		CONSTRAINT PK_GiftQuestions PRIMARY KEY,
	GiftId INT NOT NULL
		CONSTRAINT FK_GiftQuestions_Gifts FOREIGN KEY REFERENCES dbo.Gifts(GiftId),
	AccountId INT NOT NULL
		CONSTRAINT FK_GiftQuestions_Accounts FOREIGN KEY REFERENCES dbo.Accounts(AccountId),
	Timestamp DATETIME NOT NULL 
		CONSTRAINT DF_GiftQuestions_Timestamp DEFAULT GETUTCDATE(),
	QuestionText TEXT NOT NULL
)

CREATE TABLE dbo.GiftAnswers
(
	GiftAnswerId INT NOT NULL IDENTITY(1, 1)
		CONSTRAINT PK_GiftAnswers PRIMARY KEY,
	GiftQuestionId INT NOT NULL
		CONSTRAINT FK_GiftAnswers_GiftQuestions FOREIGN KEY REFERENCES dbo.GiftQuestions(GiftQuestionId),
	AccountId INT NOT NULL
		CONSTRAINT FK_GiftAnswers_Accounts FOREIGN KEY REFERENCES dbo.Accounts(AccountId),
	Timestamp DATETIME NOT NULL
		CONSTRAINT DF_GiftAnswers_Timestamp DEFAULT GETUTCDATE(),
	QuestionAnswer TEXT NOT NULL
)

CREATE TABLE dbo.Comments
(
	CommentId INT NOT NULL IDENTITY(1, 1)
		CONSTRAINT PK_Comments PRIMARY KEY,
	GiftId INT NOT NULL
		CONSTRAINT FK_Comments_Gifts FOREIGN KEY REFERENCES dbo.Gifts(GiftId),
	AccountId INT NOT NULL
		CONSTRAINT FK_Comments_Accounts FOREIGN KEY REFERENCES dbo.Accounts(AccountId),
	Timestamp DATETIME NOT NULL
		CONSTRAINT DF_Comments_Timestamp DEFAULT GETUTCDATE(),
	CommentText TEXT NOT NULL
)

";
            }
        }
    }
}