using Amazon;
using Amazon.Glacier;
using Amazon.Glacier.Model;
using Avalanche.Glacier;
using Avalanche.Lightroom;
using Avalanche.Repository;
using SevenZip;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
        const string SnsTopicId = @"arn:aws:sns:us-east-1:608438481935:email";

        public static void Main(string[] args)
        {
            var catalogLocation = @"C:\LRPortable\Catalog\Landis\Landis.lrcat";
            var avalancheFileLocation = @"C:\Junk\dropbox\Dropbox\Backup\Avalanche\avalanche.sqlite";

            var lightroomRepo = new LightroomRepository(catalogLocation);
            var output = lightroomRepo.GetAllPictures();
            var filteredPictures = output.Where(a => a.LibraryCount > 1);

            var avalancheRepo = new AvalancheRepository(avalancheFileLocation);
            var gateway = new GlacierGateway(AccessKeyId, SecretAccessKey, AccountId);

            filteredPictures = filteredPictures.Where(a => !avalancheRepo.FileIsArchived(a.FileId));

            foreach (var f in filteredPictures.Skip(1).Take(1))
            {
                Console.WriteLine("Need to archive {0}", Path.Combine(f.AbsolutePath, f.FileName));

                var archive = gateway.SaveImage(f, "Pictures-Test");
                //avalancheRepo.MarkFileAsArchived(archive);
            }

            Console.WriteLine("Done");
            Console.Read();
        }
    }
}
