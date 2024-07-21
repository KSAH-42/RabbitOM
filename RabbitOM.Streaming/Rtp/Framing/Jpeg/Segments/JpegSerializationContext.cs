using System;
using System.IO;
using System.Text;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg.Segments
{
    public sealed class JpegSerializationContext
    {
        private readonly MemoryStream _stream;





        public JpegSerializationContext( MemoryStream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
        }





        public void WriteAsByte( byte value )
        {
            _stream.WriteByte( value );
        }

        public void WriteAsUInt16( int value )
        {
            _stream.WriteByte( (byte) ( ( value >> 8 ) & 0xFF ) );
            _stream.WriteByte( (byte) ( ( value ) & 0xFF ) );
        }

        public void WriteAsUInt16( UInt16 value )
        {
            _stream.WriteByte( (byte) ( ( value >> 8 ) & 0xFF ) );
            _stream.WriteByte( (byte) ( ( value      ) & 0xFF ) );
        }

        public void WriteAsBinary( byte[] value )
        {
            if ( value == null || value.Length <= 0 )
            {
                return;
            }

            _stream.Write( value , 0 , value.Length );
        }

        public void WriteAsBinary( ArraySegment<byte> value )
        {
            if ( value.Array == null || value.Count <= 0 )
            {
                return;
            }

            _stream.Write( value.Array , value.Offset , value.Count );
        }

        public void WriteAsString( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return;
            }
            
            var buffer = Encoding.ASCII.GetBytes( value );

            if ( buffer == null || buffer.Length == 0 )
            {
                return;
            }

            _stream.Write( buffer , 0 , buffer.Length );
        }
    }
}
