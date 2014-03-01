using Amazon;
using Amazon.Glacier;
using Amazon.Glacier.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.IO;
using Newtonsoft.Json;
using SevenZip;
using Avalanche.Models;
using Amazon.Glacier.Transfer;
using Amazon.Runtime;

namespace Avalanche.Glacier
{
    public class GlacierGateway
    {
        protected readonly string _accessKeyId;
        protected readonly string _secretAccessKey;
        protected readonly string _accountId;

        public GlacierGateway(string accessKeyId, string secretAccessKey, string accountId = "-")
        {
            _accessKeyId = accessKeyId;
            _secretAccessKey = secretAccessKey;
            _accountId = accountId;
        }

        protected IAmazonGlacier GetGlacierClient()
        {
            return new AmazonGlacierClient(_accessKeyId, _secretAccessKey, RegionEndpoint.USEast1);
        }

        public void AssertVaultExists(string vaultName)
        {
            vaultName = GetTrimmedVaultName(vaultName);
            var exists = VaultExists(vaultName);

            if (exists)
            {
                Console.WriteLine("Vault {0} exists", vaultName);
                return;
            }

            Console.WriteLine("Creating vault {0}", vaultName);
            using (var client = GetGlacierClient())
            {
                var result = client.CreateVault(new CreateVaultRequest
                {
                    AccountId = _accountId,
                    VaultName = vaultName
                });
                Console.WriteLine("Vault creation result: {0}", result.HttpStatusCode);
            }
        }

        protected bool VaultExists(string vaultName)
        {
            vaultName = GetTrimmedVaultName(vaultName);

            using (var client = GetGlacierClient())
            {
                var response = client.ListVaults(new ListVaultsRequest
                {
                    AccountId = _accountId
                });

                return response.VaultList.Any(a => a.VaultName.Equals(vaultName));
            }
        }

        protected string GetTrimmedVaultName(string vaultName)
        {
            if (vaultName.IsBlank())
            {
                throw new ArgumentException("Value cannot be null/empty", "vaultName");
            }

            vaultName = vaultName.Trim();
            return vaultName;
        }

        public void SaveImage(PictureModel picture, string vaultName = "Pictures", bool compress = true)
        {
            SaveFile(Path.Combine(picture.AbsolutePath, picture.FileName), picture, vaultName, compress);
        }

        public void SaveFile(string filename, object metadata, string vaultName, bool compress)
        {
            var json = JsonConvert.SerializeObject(metadata);

            using (var fileStream = GetFileStream(filename, compress))
            {
                using (var client = GetGlacierClient())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Uploading {0}, {1} bytes", filename, fileStream.Length);
                    Console.ResetColor();

                    var hash = TreeHashGenerator.CalculateTreeHash(fileStream);
                    fileStream.Position = 0;

                    UploadArchiveResponse result;
                    using (var percentUpdater = new ConsolePercentUpdater())
                    {
                        percentUpdater.Start();

                        result = client.UploadArchive(new UploadArchiveRequest
                        {
                            AccountId = _accountId,
                            ArchiveDescription = json,
                            VaultName = GetTrimmedVaultName(vaultName),
                            Body = fileStream,
                            Checksum = hash,
                            StreamTransferProgress = new EventHandler<StreamTransferProgressArgs>((a, b) =>
                                {
                                    percentUpdater.PercentDone = b.PercentDone;
                                })
                        });
                    }

                    Console.WriteLine("File uploaded: {0}, archive ID: ", result.HttpStatusCode, result.ArchiveId);
                    Console.WriteLine("RequestId: {0}", result.ResponseMetadata.RequestId);
                    foreach (var m in result.ResponseMetadata.Metadata)
                    {
                        Console.Write("Metadata: {0}: {1}", m.Key, m.Value);
                    }
                }
            }
        }

        protected Stream GetFileStream(string filename, bool compress)
        {
            var file = File.OpenRead(filename);
            if (!compress)
            {
                return file;
            }

            SevenZipCompressor.SetLibraryPath(@"C:\Program Files\7-Zip\7z.dll");
            var compressor = new SevenZipCompressor
            {
                ArchiveFormat = OutArchiveFormat.SevenZip,
                CompressionMode = CompressionMode.Create,
                CompressionLevel = CompressionLevel.Ultra
            };
            var compressedStream = new MemoryStream();
            compressor.CompressStream(file, compressedStream);
            compressedStream.Position = 0;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Compressed {0} from {1} to {2}, removing {3:0.00}%", Path.GetFileName(filename), file.Length, compressedStream.Length, (float)((file.Length - compressedStream.Length) * 100) / file.Length);
            Console.ResetColor();
            
            return compressedStream;
        }

        public void BeginVaultInventoryRetrieval(string vaultName, string notificationTargetTopicId)
        {
            vaultName = GetTrimmedVaultName(vaultName);
            using (var client = GetGlacierClient())
            {
                var response = client.InitiateJob(new InitiateJobRequest
                {
                    VaultName = vaultName,
                    JobParameters = new JobParameters
                    {
                        Type = "inventory-retrieval",
                        SNSTopic = notificationTargetTopicId
                    }
                });
                Console.WriteLine("Job ID: {0}", response.JobId);
            }
        }

        public void PickupVaultInventoryRetrieval(string vaultName, string jobId, string outputFileName)
        {
            using(var client = GetGlacierClient())
            {
                var result = client.GetJobOutput(new GetJobOutputRequest
                {
                    AccountId = _accountId,
                    JobId = jobId,
                    VaultName = vaultName
                });

                using (var file = File.OpenWrite(outputFileName))
                {
                    result.Body.CopyTo(file);
                }
            }
        }
    }
}
