using System;
using System.IO;

namespace RabbitOM.Streaming.Rtp
{
    public sealed class RtpStreamWriter : IDisposable
    {
        private readonly MemoryStream _stream = new MemoryStream();



        public long Length { get => _stream.Length; }

        public long Position { get => _stream.Position; }

        public bool IsEmpty { get => _stream.Length <= 0; }




        public void Clear()
        {
            _stream.SetLength( 0 );
        }

        public void Close()
        {
            _stream.Close();
        }

        public void Dispose()
        {
            _stream.Dispose();
        }

        public void SetLength( long value ) 
        {
            _stream.SetLength( value > 0 ? value : 0 );
        }

        public void SetPosition( long value )
        {
            _stream.Position = Numerics.Clamp( value , 0 , _stream.Length );
        }

        public void Write( byte[] buffer )
        {
            if ( buffer == null || buffer.Length <= 0 )
            {
                return;
            }

            _stream.Write( buffer , 0 , buffer.Length );
        }

        public void Write( ArraySegment<byte> buffer )
        {
            if ( buffer.Array == null || buffer.Count <= 0 )
            {
                return;
            }

            _stream.Write( buffer.Array , buffer.Offset , buffer.Count );
        }

        public void Write( RtpStreamWriter memoryStream )
        {
            if ( memoryStream == null || memoryStream.IsEmpty )
            {
                return;
            }

            if ( memoryStream._stream.TryGetBuffer( out var buffer ) )
            {
                _stream.Write( buffer.Array , buffer.Offset , buffer.Count );
            }
        }

        public void WriteByte( byte value )
        {
            _stream.WriteByte( value );
        }

        public void WriteUInt16( int value )
        {
            _stream.WriteByte( (byte) (( value >> 8 ) & 0xFF ) );
            _stream.WriteByte( (byte) (  value & 0xFF ) );
        }

        public void WriteString( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return;
            }

            var buffer = System.Text.Encoding.UTF8.GetBytes( value );

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
