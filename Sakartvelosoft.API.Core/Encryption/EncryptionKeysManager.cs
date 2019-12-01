using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SakartveloSoft.API.Core.Encryption
{
    public class EncryptionKeysManager
    {
        public static EncryptionKeysManager Default { get; set; }

        static EncryptionKeysManager()
        {
            Default = new EncryptionKeysManager();
        }

        public virtual IKeyPair GenerateNewKeyPair()
        {
            using(var provider = new RSACryptoServiceProvider(4096))
            {
                return new ApplicationEncryptionKey(keyBinaryData:provider.ExportCspBlob(true));
            }
        }

        public virtual IKeyPair LoadKeyPair(byte[] keyPairData)
        {
            using (var provider = new RSACryptoServiceProvider())
            {
                provider.ImportCspBlob(keyPairData);
                return new ApplicationEncryptionKey(keyPairData);
            }
        }
    }
}
