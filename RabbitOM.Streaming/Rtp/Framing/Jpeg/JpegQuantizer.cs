using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public sealed class JpegQuantizer
    {
        private const int DefaultTableLength = 128;

        private ArraySegment<byte> _table;

        private int _quantizationFactor;

       
        public ArraySegment<byte> CreateDefaultTable( int quantizationFactor )
        {
            if ( _table.Count > 0 && _quantizationFactor == quantizationFactor )
            {
                return _table;
            }

            byte[] buffer = new byte[ DefaultTableLength ];

            int qFactor = AdaptQuantizationFactor( quantizationFactor );

            for ( int i = 0 ; i < buffer.Length ; ++i )
            {
                int newVal = ( JpegConstants.DefaultQuantizers[ i ] * qFactor + 50 ) / 100;

                buffer[ i ] = (newVal < 1) ? (byte) 0x01 : (newVal > 0xFF ) ? (byte) 0xFF : (byte) newVal;
            }

            _table = new ArraySegment<byte>( buffer , 0 , buffer.Length );

            _quantizationFactor = quantizationFactor;

            return _table;
        }

        private static int AdaptQuantizationFactor( int value )
        {
            value = value < 1 ? 1 : value > 99 ? 99 : value;

            return value < 50 ? ( 5000 / value ) : ( 200 - value * 2 );
        }
    }
}
