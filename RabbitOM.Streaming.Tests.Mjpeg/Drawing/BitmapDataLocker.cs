using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace RabbitOM.Streaming.Tests.Mjpeg.Drawing
{
    public struct BitmapDataLocker : IDisposable
    {
        private readonly Bitmap _bitmap;
        private readonly BitmapData _data;




        public BitmapDataLocker( Bitmap bitmap , Rectangle rect , bool useHighQuality )
            : this( bitmap , rect , ImageLockMode.ReadOnly , useHighQuality ? PixelFormat.Format32bppRgb : PixelFormat.Format24bppRgb )
        {
        }

        public BitmapDataLocker( Bitmap bitmap , Rectangle rect , ImageLockMode flags , PixelFormat format)
        {
            _bitmap = bitmap ?? throw new ArgumentNullException( nameof(bitmap) );
            _data = bitmap.LockBits( rect , flags , format );
        }

        
        
        
        public void Dispose()
            => _bitmap.UnlockBits( _data );

        public IntPtr GetScan0() 
            => _data.Scan0; 
        
        public int GetBufferSize() 
            => _data.Stride * _bitmap.Height;

        public int GetStride() 
            => _data.Stride; 
    }
}
