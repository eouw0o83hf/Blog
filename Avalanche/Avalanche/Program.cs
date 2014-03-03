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

        const string Vault = @"Pictures-Test";

        const string CatalogLocation = @"C:\LRPortable\Catalog\Landis\Landis.lrcat";
        const string AvalancheFileLocation = @"C:\Junk\dropbox\Dropbox\Backup\Avalanche\avalanche.sqlite";

        public static void Main(string[] args)
        {
            var lightroomRepo = new LightroomRepository(CatalogLocation);
            var avalancheRepo = new AvalancheRepository(AvalancheFileLocation);
            var gateway = new GlacierGateway(AccessKeyId, SecretAccessKey, AccountId);

            var catalogId = lightroomRepo.GetUniqueId();
            var allPictures = lightroomRepo.GetAllPictures();
            var filteredPictures = allPictures.Where(a => a.LibraryCount > 1);
            filteredPictures = filteredPictures.Where(a => !avalancheRepo.FileIsArchived(a.FileId));

            foreach (var f in filteredPictures)
            {
                Console.WriteLine("Need to archive {0}", Path.Combine(f.AbsolutePath, f.FileName));

                var archive = gateway.SaveImage(f, Vault);
                avalancheRepo.MarkFileAsArchived(archive, Vault, "", CatalogLocation, catalogId.ToString());
            }

            Console.WriteLine("Done");
            Console.Read();
        }
    }
}
