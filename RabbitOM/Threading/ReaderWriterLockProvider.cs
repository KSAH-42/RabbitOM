using System;
using System.Threading;

namespace RabbitOM.Threading
{
    public sealed class ReaderWriterLockProvider : IDisposable
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim( LockRecursionPolicy.SupportsRecursion );

        public IDisposable ReaderLock
        {
            get
            {
                _lock.EnterReadLock();

                return new Disposer( _lock.ExitReadLock );
            }
        }

        public IDisposable WriterLock
        {
            get
            {
                _lock.EnterWriteLock();

                return new Disposer( _lock.ExitWriteLock );
            }
        }

        public void Dispose()
        {
            _lock.Dispose();
        }
    }
}
