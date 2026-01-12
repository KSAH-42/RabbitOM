using System;

namespace RabbitOM.Streaming
{
    /// <summary>
    /// Represent the resolution info
    /// </summary>
    public struct ResolutionInfo
    {
        /// <summary>
        /// A resolution
        /// </summary>
        public static readonly ResolutionInfo Resolution_800x600 = new ResolutionInfo( 800 , 600 );

        /// <summary>
        /// A resolution
        /// </summary>
        public static readonly ResolutionInfo Resolution_1280x960 = new ResolutionInfo( 1280 , 960 );

        /// <summary>
        /// A resolution
        /// </summary>
        public static readonly ResolutionInfo Resolution_1792x112 = new ResolutionInfo( 1792 , 112 );

        /// <summary>
        /// A resolution
        /// </summary>
        public static readonly ResolutionInfo Resolution_2040x2040 = new ResolutionInfo( 2040 , 2040 );

        /// <summary>
        /// A resolution
        /// </summary>
        public static readonly ResolutionInfo Resolution_3840x2160 = new ResolutionInfo( 3840 , 2160 );





        /// <summary>
        /// Initialize an instance of the resolution info
        /// </summary>
        /// <param name="width">the width</param>
        /// <param name="height">the height</param>
        /// <exception cref="ArgumentException"/>
        public ResolutionInfo( int width , int height )
        {
            if ( width < 2 )
            {
                throw new ArgumentException( nameof( width ) );
            }

            if ( height < 2 )
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
