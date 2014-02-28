using Amazon;
using Amazon.Glacier;
using Amazon.Glacier.Model;
using Avalanche.Glacier;
using SevenZip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalanche
{
    public class Program
    {
        const string AccountId = "-";
        const string AccessKeyId = @"AKIAJYQTERPFN4YKLG5A";
        const string SecretAccessKey = @"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";

        public static void Main(string[] args)
        {
            //var repo = new Lightroom.LrRepository(@"C:\Junk\LaptopCatalog1-2.lrcat");
            //var output = repo.GetAllPictures();
            //Console.WriteLine(output.Count);

            //foreach (var o in output.Where(a => a.LibraryCount > 0))
            //{
            //    var exists = System.IO.File.Exists(Path.Combine(o.AbsolutePath, o.FileName));
            //    if (!exists)
            //    {
            //        Console.WriteLine("FailE!!!");
            //    }
            //}

            //Console.Write("Done");
            //Console.Read();

            var gateway = new GlacierGateway(AccessKeyId, SecretAccessKey, AccountId);

            var repo = new Lightroom.LrRepository(@"C:\Junk\LaptopCatalog1-2.lrcat");
            var pictures = repo.GetAllPictures();

            gateway.AssertVaultExists("Pictures-Test");
            var picture = pictures.FirstOrDefault(a => a.LibraryCount > 0);
            gateway.SaveImage(picture, "Pictures-Test", false);


            Console.WriteLine("Done");
            Console.Read();
        }
    }
}
