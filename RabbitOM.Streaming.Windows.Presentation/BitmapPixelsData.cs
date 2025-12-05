using System;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace RabbitOM.Streaming.Windows.Presentation
{
    /// <summary>
    /// Represent bitmap locker used to lock and protect the modification of bitmao
    /// </summary>
    public struct BitmapPixelsData
    {
        /// <summary>
        /// Initialize a new instance pixel info 
        /// </summary>
        /// <param name="buffer">the pixels buffer</param>
        /// <param name="stride">the number of pixel per line</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public BitmapPixelsData( byte[] buffer , int stride )
        {
            if ( buffer == null )
            {
                throw new ArgumentNullException( nameof( buffer ) );
            }

            if ( buffer.Length == 0 )
            {
                throw new ArgumentException( nameof( buffer ) );
            }

            if ( stride <= 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( stride ) );
            }

            Buffer = buffer;
            Stride = stride;
        }







        /// <summary>
        /// Gets the buffer
        /// </summary>
        public byte[] Buffer { get; }
        
        /// <summary>
        /// Gets the stride
        /// </summary>
        public int Stride { get; }
        





        
        /// <summary>
        /// Try create a new instance
        /// </summary>
        /// <param name="source">the bitmap source</param>
        /// <param name="rectangle">the rectangle</param>
        /// <param name="result">the new instance</param>
        /// <returns>returns true, othewise false</returns>
        public static bool TryCreate( BitmapSource source , Rectangle rectangle , out BitmapPixelsData result )
        {
            result = default;

            if ( source == null )
            {
                return false;
            }

            if ( source.PixelWidth <= 0 || source.PixelHeight <= 0 || source.Format.BitsPerPixel <= 0 )
            {
                return false;
            }

            var stride = source.PixelWidth * ( source.Format.BitsPerPixel / 8 );

            if ( stride <= 0 )
            {
                return false;
            }

            var buffer = new byte[ stride * source.PixelHeight ];
            
            source.CopyPixels( buffer , stride , 0 );

            result = new BitmapPixelsData( buffer , stride );

            return true;
        }
    }
}
