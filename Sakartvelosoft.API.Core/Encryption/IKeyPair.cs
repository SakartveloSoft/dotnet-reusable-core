using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Encryption
{
    public interface IKeyPair
    {
        byte[] AsBinaryData();

    }
}
