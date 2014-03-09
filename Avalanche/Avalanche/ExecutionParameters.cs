﻿using log4net;
using Mono.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalanche
{
    public class ExecutionParameters
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Program));

        public class GlacierParameters
        {
            public string AccountId { get; set; }
            public string AccessKeyId { get; set; }
            public string SecretAccessKey { get; set; }

            public string SnsTopicId { get; set; }

            public string VaultName { get; set; }
        }

        public class AvalancheParameters
        {
            public string CatalongFilePath { get; set; }
            public string AvalancheFilePath { get; set; }
        }

        public GlacierParameters Glacier { get; set; }
        public AvalancheParameters Avalanche { get; set; }

        public static ExecutionParameters Initialize(string[] args)
        {
            var context = new ExecutionParameters
            {
                Glacier = new GlacierParameters(),
                Avalanche = new AvalancheParameters()
            };

            var showHelp = false;
            var options = new OptionSet
            {
                { "gk|glacier-key", "Access Key ID for Amazon Glacier", a => context.Glacier.AccessKeyId = a },
                { "gs|glacier-secret", "Secret Access Key for Amazon Glacier", a => context.Glacier.SecretAccessKey = a },
                { "ga|glacier-account", "Account ID for Amazon Glacier", a => context.Glacier.AccountId = a },
                { "gs|glacier-sns-topic", "SNS Topic ID for Amazon Glacier Job", a => context.Glacier.SnsTopicId = a },
                { "gv|glacier-vault", "Vault name for Amazon Glacier", a => context.Glacier.VaultName = a },
                { "lc|lightroom-catalog", "Path/File for Lightroom Catalog", a => context.Avalanche.CatalongFilePath = a },
                { "ad|avalanche-db", "Path/File for Avalanche DB", a => context.Avalanche.AvalancheFilePath = a },
                { "h|help", "Help", a => showHelp = a != null }
            };

            try
            {
                var extra = options.Parse(args);
                if (extra.Any())
                {
                    _log.Error("Error: following arguments were not understood: (If you need a list, use -h for help)");
                    foreach (var e in extra)
                    {
                        _log.Error(e);
                    }
                    return null;
                }
            }
            catch (OptionException ex)
            {
                _log.Error("Error with arguments: (If you need a list, use -h for help)");
                _log.Error(ex.Message);
            }

            return context;
        }
    }
}
