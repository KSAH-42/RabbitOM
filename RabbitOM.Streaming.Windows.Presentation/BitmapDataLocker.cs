using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace RabbitOM.Streaming.Windows.Presentation
{
    /// <summary>
    /// Represent bitmap locker used to lock and protect the modification of bitmao
    /// </summary>
    public struct BitmapDataLocker : IDisposable
    {
        private readonly Bitmap _bitmap;
        private readonly BitmapData _data;




        /// <summary>
        /// Initialize a new instance of the bitmal locker
        /// </summary>
        /// <param name="bitmap">the bitmap</param>
        /// <param name="rectangle">the zone where pixels will modified</param>
        /// <param name="useHighQuality">set true to improve the quality</param>
        public BitmapDataLocker( Bitmap bitmap , Rectangle rectangle , bool useHighQuality )
            : this( bitmap , rectangle , ImageLockMode.ReadOnly , useHighQuality ? PixelFormat.Format32bppRgb : PixelFormat.Format24bppRgb )
        {
        }

        /// <summary>
        /// Initialize a new instance of the bitmal locker
        /// </summary>
        /// <param name="bitmap">the bitmal</param>
        /// <param name="rectangle">the zone where pixels will modified</param>
        /// <param name="flags">the flags</param>
        /// <param name="format">the format</param>
        /// <exception cref="ArgumentNullException"/>
        public BitmapDataLocker( Bitmap bitmap , Rectangle rectangle , ImageLockMode flags , PixelFormat format)
        {
            _bitmap = bitmap ?? throw new ArgumentNullException( nameof(bitmap) );
            _data = bitmap.LockBits( rectangle , flags , format );
        }
        



        /// <summary>
        /// Gets the scan
        /// </summary>
        /// <returns>returns the size</returns>
        public IntPtr GetScan0() 
            => _data.Scan0; 
        
        /// <summary>
        /// Gets the buffer size
        /// </summary>
        /// <returns>returns the size</returns>
        public int GetBufferSize() 
            => _data.Stride * _bitmap.Height;

        /// <summary>
        /// Gets the stride
        /// </summary>
        /// <returns>return a value</returns>
        public int GetStride() 
            => _data.Stride; 

        /// <summary>
        /// Un lock the bitmap
        /// </summary>
        public void Dispose()
            => _bitmap.UnlockBits( _data );
    }
}
