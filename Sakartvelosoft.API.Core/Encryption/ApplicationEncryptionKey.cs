using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Encryption
{
    class ApplicationEncryptionKey : IKeyPair
    {
        private byte[] binaryDataCache = null;

        public ApplicationEncryptionKey(byte[] keyBinaryData)
        {
            binaryDataCache = keyBinaryData;
        }

        public byte[] AsBinaryData()
        {
            var ret = new byte[binaryDataCache.Length];
            Array.Copy(binaryDataCache, 0, ret, 0, binaryDataCache.Length);
            return ret;
        }
    }
}
