using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    /// <summary>
    /// Represent the quantization table factory class
    /// </summary>
    public sealed class JpegQuantizationTableFactory
    {
        private static readonly byte[] BaseTable =
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

        private int _factor;






        /// <summary>
        /// Generate a new table
        /// </summary>
        /// <param name="factor">the factor</param>
        /// <returns>returns a table</returns>
        public ArraySegment<byte> CreateTable( int factor )
        {
            if ( _table.Count > 0 && _factor == factor )
            {
                return _table;
            }

            byte[] buffer = new byte[ BaseTable.Length ];

            int quantizationFactor = JpegQuantizer.AdaptFactor( factor );

            for ( int i = 0 ; i < buffer.Length ; ++i )
            {
                buffer[ i ] = JpegQuantizer.Quantize( BaseTable[ i ] , quantizationFactor );
            }

            _table = new ArraySegment<byte>( buffer , 0 , buffer.Length );

            _factor = factor;

            return _table;
        }
    }
}
