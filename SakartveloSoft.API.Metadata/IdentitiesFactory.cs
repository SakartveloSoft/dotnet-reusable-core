using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SakartveloSoft.API.Metadata
{
    public class IdentitiesFactory
    {
        private readonly RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        public string GeneratePrefixedId(string idPrefix)
        {
            var bytes = new byte[sizeof(ulong) * 2];
            rng.GetBytes(bytes);
            var mem = bytes.AsSpan();
            var p1 = BitConverter.ToUInt64(mem.Slice(0, sizeof(ulong)));
            var p2 = BitConverter.ToUInt64(mem.Slice(sizeof(ulong), sizeof(ulong)));
            var result = new StringBuilder();
            Base36.ToBase62(p1, result);
            Base36.ToBase62(p2, result);
            result.Insert(0, '_');
            result.Insert(0, idPrefix);
            return result.ToString();
        }


        public string GenerateCompactPrefixedId(string idPrefix)
        {
            var bytes = new byte[8];
            rng.GetBytes(bytes);
            var mem = bytes.AsSpan();
            return $@"{idPrefix}_{Base36.ToBase62(BitConverter.ToUInt64(mem))}";

        }
    }
}
