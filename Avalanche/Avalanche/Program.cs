using Amazon;
using Amazon.Glacier;
using Amazon.Glacier.Model;
using Avalanche.Glacier;
using Avalanche.Lightroom;
using Avalanche.Models;
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

        const string Vault = @"Pictures-Raw";

        const string CatalogLocation = @"D:\Pictures\Catalogs\LaptopCatalog1-2.lrcat";
        const string AvalancheFileLocation = @"X:\CloudSync\Dropbox\Backup\Avalanche\avalanche.sqlite";

        public static void Main(string[] args)
        {
            var lightroomRepo = new LightroomRepository(CatalogLocation);
            var avalancheRepo = new AvalancheRepository(AvalancheFileLocation);
            var gateway = new GlacierGateway(AccessKeyId, SecretAccessKey, AccountId);

            var catalogId = lightroomRepo.GetUniqueId();
            var allPictures = lightroomRepo.GetAllPictures();
            var filteredPictures = allPictures.Where(a => a.LibraryCount > 1 && !avalancheRepo.FileIsArchived(a.FileId)).ToList();

            Console.WriteLine("Backing up {0} images", filteredPictures.Count);

            foreach (var f in filteredPictures)
            {
                Console.WriteLine("Need to archive {0}", Path.Combine(f.AbsolutePath, f.FileName));

                // Try three times
                ArchivedPictureModel archive = null;
                for (var i = 0; i < 3; ++i)
                {
                    try
                    {
                        archive = gateway.SaveImage(f, Vault);
                    }
                    catch(Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.WriteLine("Error!!! {0}", ex);
                        Console.ResetColor();
                        continue;
                    }
                    break;
                }

                if (archive == null)
                {
                    continue;
                }

                avalancheRepo.MarkFileAsArchived(archive, Vault, "USEast1", CatalogLocation, catalogId.ToString());
            }

            Console.WriteLine("Done");
            Console.Read();
        }
    }
}
