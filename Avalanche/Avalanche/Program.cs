﻿using Amazon;
using Amazon.Glacier;
using Amazon.Glacier.Model;
using Avalanche.Glacier;
using Avalanche.Lightroom;
using Avalanche.Models;
using Avalanche.Repository;
using log4net;
using log4net.Config;
using Mono.Options;
using SevenZip;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Avalanche
{
    public class Program
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Program));

        const string AccountId = "-";
        const string AccessKeyId = @"AKIAJYQTERPFN4YKLG5A";
        const string SecretAccessKey = @"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";
        const string SnsTopicId = @"arn:aws:sns:us-east-1:608438481935:email";

        const string Vault = @"Pictures-Raw";

        const string CatalogLocation = @"D:\Pictures\Catalogs\LaptopCatalog1-2.lrcat";
        const string AvalancheFileLocation = @"X:\CloudSync\Dropbox\Backup\Avalanche\avalanche.sqlite";

        public static void Main(string[] args)
        {
            var previousExecutionState = ThreadState.SetThreadExecutionState(ThreadState.ES_CONTINUOUS | ThreadState.ES_SYSTEM_REQUIRED);
            if (previousExecutionState == 0)
            {
                _log.Warn("Couldn't set thread state; the application may be unable to prevent the computer's going to sleep.");
            }

            try
            {
                var lightroomRepo = new LightroomRepository(CatalogLocation);
                var avalancheRepo = new AvalancheRepository(AvalancheFileLocation);
                var gateway = new GlacierGateway(AccessKeyId, SecretAccessKey, AccountId);

                var catalogId = lightroomRepo.GetUniqueId();
                var allPictures = lightroomRepo.GetAllPictures();
                var filteredPictures = allPictures.Where(a => a.LibraryCount > 0 && !avalancheRepo.FileIsArchived(a.FileId)).ToList();

                _log.InfoFormat("Backing up {0} images", filteredPictures.Count);

                var index = 0;
                foreach (var f in filteredPictures)
                {
                    _log.InfoFormat("Archiving {0}/{1}: {2}", ++index, filteredPictures.Count, Path.Combine(f.AbsolutePath, f.FileName));

                    // Try three times
                    ArchivedPictureModel archive = null;
                    for (var i = 0; i < 3; ++i)
                    {
                        try
                        {
                            archive = gateway.SaveImage(f, Vault);
                        }
                        catch (Exception ex)
                        {
                            _log.ErrorFormat("Error!!! {0}", ex);
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

                _log.Info("Done");
                Console.Read();
            }
            finally
            {
                // Restore previous state
                ThreadState.SetThreadExecutionState(previousExecutionState);
            }
        }

        static class ThreadState
        {
            [DllImport("kernel32.dll")]
            public static extern uint SetThreadExecutionState(uint esFlags);
            public const uint ES_CONTINUOUS = 0x80000000;
            public const uint ES_SYSTEM_REQUIRED = 0x00000001;
        }
    }
}