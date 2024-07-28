using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public sealed class JpegQuantizer
    {
        private static readonly byte[] DefaultQuantizer =
        {
            16, 11, 12, 14, 12, 10, 16, 14,
            13, 14, 18, 17, 16, 19, 24, 40,
            26, 24, 22, 22, 24, 49, 35, 37,
            29, 40, 58, 51, 61, 60, 57, 51,
            56, 55, 64, 72, 92, 78, 64, 68,
            87, 69, 55, 56, 80, 109, 81, 87,
            95, 98, 103, 104, 103, 62, 77, 113,
            121, 112, 100, 120, 92, 101, 103, 99,
            17, 18, 18, 24, 21, 24, 47, 26,
            26, 47, 99, 66, 56, 66, 99, 99,
            99, 99, 99, 99, 99, 99, 99, 99,
            99, 99, 99, 99, 99, 99, 99, 99,
            99, 99, 99, 99, 99, 99, 99, 99,
            99, 99, 99, 99, 99, 99, 99, 99,
            99, 99, 99, 99, 99, 99, 99, 99,
            99, 99, 99, 99, 99, 99, 99, 99
        };




        private ArraySegment<byte> _table;

        private int _quantizationFactor;




        public ArraySegment<byte> CreateTable( int quantizationFactor )
        {
            if ( _table.Count > 0 && _quantizationFactor == quantizationFactor )
            {
                return _table;
            }

            byte[] buffer = new byte[ DefaultQuantizer.Length ];

            int factor = AdaptQuantizationFactor( quantizationFactor );

            for ( int i = 0 ; i < buffer.Length ; ++i )
            {
                int newVal = ( DefaultQuantizer[ i ] * factor + 50 ) / 100;

                buffer[ i ] = (newVal < 1) ? (byte) 0x01 : (newVal > 0xFF ) ? (byte) 0xFF : (byte) newVal;
            }

            _table = new ArraySegment<byte>( buffer , 0 , buffer.Length );

            _quantizationFactor = quantizationFactor;

            return _table;
        }






        private static int AdaptQuantizationFactor( int value )
        {
            value = value < 1 ? 1 : (value > 99 ? 99 : value);

            return value < 50 ? ( 5000 / value ) : ( 200 - value * 2 );
        }
    }
}
