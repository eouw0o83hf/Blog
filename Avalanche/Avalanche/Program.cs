using Amazon;
using Amazon.Glacier;
using Amazon.Glacier.Model;
using System;
using System.Collections.Generic;
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
            var client = new AmazonGlacierClient(AccessKeyId, SecretAccessKey, RegionEndpoint.USEast1);
            var request = new ListVaultsRequest
            {
                AccountId = AccountId
            };
            var response = client.ListVaults(request);

            Console.WriteLine("Dumping stuff");
            foreach (var v in response.VaultList)
            {
                Console.WriteLine(v.VaultName);

                var request2 = new DescribeVaultRequest
                {
                    AccountId = AccountId,
                    VaultName = v.VaultName
                };
                var response2 = client.DescribeVault(request2);
                
            }
            Console.WriteLine("Done");
            Console.Read();
        }
    }
}
