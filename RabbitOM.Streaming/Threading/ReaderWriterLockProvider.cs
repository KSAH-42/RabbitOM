using System;
using System.Threading;

namespace RabbitOM.Streaming.Threading
{
    /// <summary>
    /// Represent a provider class to deliver customs lock for protection regions during concurrent calls on read or write operation on regions
    /// </summary>
    public sealed class ReaderWriterLockProvider : IDisposable
    {
        private readonly ReaderWriterLockSlim _lock;
        private readonly Disposer _readerLock;
        private readonly Disposer _writerLock;




        /// <summary>
        /// Initialize a new instance of the reader writer locks provider
        /// </summary>
        public ReaderWriterLockProvider()
        {
            _lock = new ReaderWriterLockSlim( LockRecursionPolicy.SupportsRecursion );
            _readerLock = new Disposer( _lock.ExitReadLock );
            _writerLock = new Disposer( _lock.ExitWriteLock );
        } 
        



        /// <summary>
        /// Gets the reader lock
        /// </summary>
        public IDisposable ReaderLock
        {
            get
            {
                _lock.EnterReadLock();

                return _readerLock;
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

                return _writerLock;
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
