using System;
using System.Threading;

namespace RabbitOM.Streaming.Threading
{
    /// <summary>
    /// Represent a provider class to deliver customs lock for protection regions during concurrent calls on read or write operation on regions
    /// </summary>
    public sealed class ReaderWriterLockProvider : IDisposable
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim( LockRecursionPolicy.SupportsRecursion );

        /// <summary>
        /// Gets the reader lock
        /// </summary>
        public IDisposable ReaderLock
        {
            get
            {
                _lock.EnterReadLock();

                return new Disposer( _lock.ExitReadLock );
            }
        }

        /// <summary>
        /// Gets the writer lock
        /// </summary>
        public IDisposable WriterLock
        {
            get
            {
                _lock.EnterWriteLock();

                return new Disposer( _lock.ExitWriteLock );
            }
        }

        /// <summary>
        /// Release resources
        /// </summary>
        public void Dispose()
        {
            _lock.Dispose();
        }
    }
}
