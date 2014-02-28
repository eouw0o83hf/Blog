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
                    Console.WriteLine("uploading {0}", json);

                    //client.UploadArchive(new UploadArchiveRequest
                    //{
                    //    AccountId = _accountId,
                    //    ArchiveDescription = json,
                    //    VaultName = GetTrimmedVaultName(vaultName),
                    //    Body = fileStream
                    //});
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

            var compressor = new SevenZipCompressor
            {
                ArchiveFormat = OutArchiveFormat.SevenZip,
                CompressionMode = CompressionMode.Create,
                CompressionLevel = CompressionLevel.Ultra
            };
            var compressedStream = new MemoryStream();
            compressor.CompressStream(file, compressedStream);
            compressedStream.Position = 0;

            return compressedStream;
        }

        protected void x()
        {
            var client = GetGlacierClient();
            var request = new ListVaultsRequest
            {
                AccountId = _accountId
            };
            var response = client.ListVaults(request);

            Console.WriteLine("Dumping stuff");
            foreach (var v in response.VaultList)
            {
                Console.WriteLine(v.VaultName);

                var request2 = new DescribeVaultRequest
                {
                    AccountId = _accessKeyId,
                    VaultName = v.VaultName
                };
                var response2 = client.DescribeVault(request2);

            }
            Console.WriteLine("Done");
            Console.Read();
        }
    }
}
