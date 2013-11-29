using SimpleCrypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiftGivr.Web.Classes
{
    public class CryptoProvider
    {
        private const int HashIterations = 10000;
        private const int SaltSize = 128;
        private readonly ICryptoService _cryptoService;

        public CryptoProvider(ICryptoService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        public string GetNewSalt()
        {
            return _cryptoService.GenerateSalt(HashIterations, SaltSize);
        }

        public string HashPassword(string password, string salt)
        {
            return _cryptoService.Compute(password, salt);
        }

        public string CreateNewPassword()
        {
            return Guid.NewGuid()
                        .ToString()
                        .Replace("-", string.Empty)
                        .Substring(0, 10);
        }
    }
}