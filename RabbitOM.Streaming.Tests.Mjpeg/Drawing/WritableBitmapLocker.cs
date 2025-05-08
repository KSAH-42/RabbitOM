using System;
using System.Windows.Media.Imaging;

namespace RabbitOM.Streaming.Tests.Mjpeg.Drawing
{
    public struct WritableBitmapLocker : IDisposable
    {
        private readonly WriteableBitmap _writableBitmap;

        public WritableBitmapLocker( WriteableBitmap writableBitmap )
        {
            _writableBitmap = writableBitmap ?? throw new ArgumentNullException( nameof(writableBitmap) );
            _writableBitmap.Lock();
        }

        public void Dispose()
        {
            _writableBitmap.Unlock();
        }
    }
}
