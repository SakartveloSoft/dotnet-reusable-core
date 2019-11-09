using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Framework.Adapters
{
    public class DisposableCallback : IDisposable
    {
        private Action callback;
        private bool disposed;

        public DisposableCallback(Action callbackImpl)
        {
            callback = callbackImpl;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool v)
        {
            if (disposed)
            {
                throw new InvalidOperationException("Disposable callback disposed 2 times");
            }
            disposed = true;
        }
    }
}
