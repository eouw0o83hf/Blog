using DbSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Migrations
{
    public class M002_AddInitialPosts : IMigration
    {
        public long Version
        {
            get { return 2; }
        }

        public string SqlCommand
        {
            get
            {
                return @"

INSERT INTO Blogs
SELECT
	'eouw0o83hf',
	'eouw0o83hf Blog',
	'eouw0o83hf Blog : Nathan Landis''s personal blog'

DECLARE @BlogId INT = SCOPE_IDENTITY()

INSERT INTO Posts
SELECT
	@BlogId,
	NEWID(),
	'Initial-Post',
	'Initial Post',
	N'
Hello world, this is my first post. There should be a bucket of links on the bottom of each page now, so check out the other stuff.

Next to come are:

- Simple styling (kill the serif default, etc.)
- Simple JS: Let code be prettified!',
	'2013-03-10 20:30:00',
	'Nathan Landis'

INSERT INTO Posts
SELECT
	@BlogId,
	NEWID(),
	'FizzBuzz-Code-PoC',
	'FizzBuzz (Code PoC)',
	N'
Okay, let''s test out some code styling. Here''s a fizz-buzz implementation (with a couple of liberties taken to get it to smash down to one line):

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine
            (
                string.Join
                (
                    Environment.Newline, 
                    Enumerable.Range(1, 100)
                        .Select(a => 
                            a.ToString() + "": "" +
                            (a % 3 == 0 ? ""Fizz"" : string.Empty) + 
                            (a % 5 == 0 ? ""Buzz"" : string.Empty)
                        )
                )
            );
            Console.Read();
        }
    }',
	'2013-03-10 20:45:00',
	'Nathan Landis'



INSERT INTO Posts
SELECT
	@BlogId,
	NEWID(),
	'Trello-Link',
	'Trello Link',
	N'I''ve just added [a link to the Trello board](https://trello.com/board/eouw0o83hf-com/513f9eae3db9e83015000ce1 ""Trello board"") I''m be using to track the development of this site: both actual code development and content.',
	'2013-03-13 22:06:00',
	'Nathan Landis'

";
            }
        }
    }
}
