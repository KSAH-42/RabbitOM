using System;
using System.Windows.Media.Imaging;

namespace RabbitOM.Streaming.Windows.Presentation
{
    /// <summary>
    /// Represent writable bitmap locker
    /// </summary>
    public struct WritableBitmapLocker : IDisposable
    {
        private readonly WriteableBitmap _writableBitmap;

        /// <summary>
        /// Initialize a new instance of the locker
        /// </summary>
        /// <param name="writableBitmap">the writable bitmap</param>
        /// <exception cref="ArgumentNullException"/>
        public WritableBitmapLocker( WriteableBitmap writableBitmap )
        {
            _writableBitmap = writableBitmap ?? throw new ArgumentNullException( nameof(writableBitmap) );
            _writableBitmap.Lock();
        }

        /// <summary>
        /// Unlock the writable bitmap
        /// </summary>
        public void Dispose()
            =>_writableBitmap.Unlock();
    }
}
