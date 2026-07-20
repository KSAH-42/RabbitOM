
using System;

namespace RabbitOM
{
    public struct ResolutionInfo
    {
        public static readonly ResolutionInfo Resolution_800x600 = new ResolutionInfo( 800 , 600 );

        public static readonly ResolutionInfo Resolution_1280x960 = new ResolutionInfo( 1280 , 960 );

        public static readonly ResolutionInfo Resolution_1792x112 = new ResolutionInfo( 1792 , 112 );

        public static readonly ResolutionInfo Resolution_2040x2040 = new ResolutionInfo( 2040 , 2040 );

        public static readonly ResolutionInfo Resolution_3840x2160 = new ResolutionInfo( 3840 , 2160 );




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




        public int Width { get; }

        public int Height { get; }
    }
}
