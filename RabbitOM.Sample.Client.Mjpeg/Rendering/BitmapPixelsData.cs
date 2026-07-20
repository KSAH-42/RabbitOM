using System;
using System.Windows.Media.Imaging;

namespace RabbitOM.Sample.Client.Mjpeg.Rendering
{
    public struct BitmapPixelsData
    {
        public static readonly BitmapPixelsData Empty = new BitmapPixelsData();




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




        public byte[] Buffer { get; }

        public int Stride { get; }




        public static bool IsNullOrEmpty( BitmapPixelsData pixelsData )
            => pixelsData.Buffer == null || pixelsData.Buffer.Length == 0 || pixelsData.Stride <= 0;

        public static bool IsNullOrInvalid( BitmapSource source  )
            => source == null || source.PixelWidth <= 0 || source.PixelHeight <= 0 || source.Format.BitsPerPixel <= 0;

        public static bool TryCreate( BitmapSource source , out BitmapPixelsData result )
        {
            result = default;

            if ( IsNullOrInvalid( source ) )
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

        public static bool TryCopy( BitmapSource source , ref BitmapPixelsData pixelsData )
        {
            if ( IsNullOrInvalid( source ) )
            {
                return false;
            }

            var stride = source.PixelWidth * ( source.Format.BitsPerPixel / 8 );

            if ( stride <= 0 || pixelsData.Stride != stride  )
            {
                return false;
            }

            var length = stride * source.PixelHeight;

            if ( length <= 0 || pixelsData.Buffer.Length != length )
            {
                return false;
            }

            source.CopyPixels( pixelsData.Buffer , pixelsData.Stride , 0 );

            return true;
        }
    }
}
