using Blog.Web.ViewModels.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Web.Controllers
{
    public class BlogController : Controller
    {
        protected IEnumerable<Post> getPosts()
        {
            yield return new Post
            {
                Id = 3,
                Title = "Trello Link",
                PostDate = new DateTime(2013, 3, 13, 17, 6, 0),
                RawBody = @"
<p>I've just added <a href=""https://trello.com/board/eouw0o83hf-com/513f9eae3db9e83015000ce1"">a link to the Trello board</a> I'm be using to track the development of this site: both actual code development and content.</p>"
            };

            yield return new Post
            {
                Id = 2,
                Title = "FizzBuzz (Code PoC)",
                PostDate = new DateTime(2013, 03, 10, 15, 45, 0),
                RawBody = @"
<p>Okay, let's test out some code styling. Here's a fizz-buzz implementation (with a couple of liberties taken to get it to smash down to one line):</p>
<pre class=""prettyprint"">
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
}
</pre>
"
            };

            yield return new Post
            {
                Id = 1,
                Title = "My Title Here",
                PostDate = new DateTime(2013, 3, 10, 15, 30, 0),
                RawBody = @"
<p>Hello world, this is my first post. There should be a bucket of links on the bottom of each page now, so check out the other stuff.</p>
<p>Next to come are:</p>
<ul>
    <li>Simple styling (kill the serif default, etc.)</li>
    <li>Simple JS: Let code be prettified!</li>
</ul>
"
            };
        }

        [HttpGet]
        public ActionResult Page()
        {
            var result = new Page
            {
                BlogName = "eouw0o83hf.com Blog",
                Posts = getPosts().ToList()
            };
            return View(result);
        }
    }
}
