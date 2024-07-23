using System;
using System.IO;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public sealed class JpegMemoryStream : IDisposable
    {
        private readonly MemoryStream _stream = new MemoryStream( 1024 * 5 );


        public bool IsEmpty { get => _stream.Length == 0; }
        public long Capacity { get => _stream.Capacity; }
        public long Length { get => _stream.Length; }
        

        public void Dispose() 
        {
            _stream.Dispose();
        }
        
        public void Clear()
        {
            _stream.SetLength( 0 );
        }
        
        public void SetLength( long value ) 
        {
            _stream.SetLength( value );
        }
        
        public void WriteAsByte( byte value ) 
        {
            _stream.WriteByte( value );
        }
        
        public void WriteAsUInt16( int value )
        {
            _stream.WriteByte( (byte) (( value >> 8 ) & 0xFF ) );
            _stream.WriteByte( (byte) (  value & 0xFF ) );
        }
        
        public void WriteAsBinary( byte[] buffer )
        {
            if ( buffer == null || buffer.Length == 0 )
            {
                return;
            }

            _stream.Write( buffer , 0 , buffer.Length );
        }

        public void WriteAsBinary( byte[] buffer , int offset , int count )
        {
            if ( buffer == null || buffer.Length == 0 )
            {
                return;
            }

            int delta = count - offset;

            if ( 0 < delta && delta <= buffer.Length )
            {
                _stream.Write( buffer , offset , count );
            }
        }
        
        public void WriteAsBinary( ArraySegment<byte> buffer )
        {
            if ( buffer.Count > 0 )
            {
                _stream.Write( buffer.Array , buffer.Offset , buffer.Count );
            }
        }

        public void WriteAsString( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return;
            }

            var buffer = System.Text.Encoding.ASCII.GetBytes( value );

            if ( buffer.Length > 0 )
            {
                _stream.Write( buffer , 0 , buffer.Length );
            }
        }
        
        public byte[] ToArray() 
        {
            return _stream.ToArray();
        }
    }
}
