using System;

namespace RabbitOM.Streaming.Net.Rtp.Jpeg.Imaging
{
    /// <summary>
    /// Represent the jpeg resolution info
    /// </summary>
    public struct JpegResolution
    {
        /// <summary>
        /// A resolution
        /// </summary>
        public static readonly JpegResolution Resolution_1280x960 = new JpegResolution( 1280 , 960 );

        /// <summary>
        /// A resolution
        /// </summary>
        public static readonly JpegResolution Resolution_2040x2040 = new JpegResolution( 2040 , 2040 );

        /// <summary>
        /// A resolution
        /// </summary>
        public static readonly JpegResolution Resolution_3840x2160 = new JpegResolution( 3840 , 2160 );





        /// <summary>
        /// Initialize an instance of the resolution info
        /// </summary>
        /// <param name="width">the width</param>
        /// <param name="height">the height</param>
        /// <exception cref="ArgumentException"/>
        public JpegResolution( int width , int height )
        {
            if ( width <= 2 )
            {
                throw new ArgumentException( nameof( width ) );
            }

            if ( height <= 2 )
            {
                throw new ArgumentException( nameof( height ) );
            }

            Width = width;
            Height = height;
        }





        /// <summary>
        /// Gets the width
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the height
        /// </summary>
        public int Height { get; private set; }
    }
}
