using System;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265StreamWriter : IDisposable
    {
        private static readonly byte[] StartMarker = { 0x00 , 0x00 , 0x01 };

        
        private readonly RtpMemoryStream _stream = new RtpMemoryStream();






        public long Length
        {
            get => _stream.Length;
        }






        public void Dispose()
        {
            _stream.Dispose();
        }

        public void Write( ArraySegment<byte> data )
        {
            if ( data.Count == 0 )
            {
                throw new ArgumentException( nameof( data ) );
            }

            _stream.WriteAsBinary( data );
        }

        public void WriteParameterSet( ArraySegment<byte> data )
        {
            if ( data.Count == 0 )
            {
                throw new ArgumentException( nameof( data ) );
            }

            _stream.WriteAsBinary( StartMarker );
            _stream.WriteAsBinary( data.Array , data.Offset , data.Count );
        }

        public byte[] ToArray()
        {
            return _stream.ToArray();
        }
    }
}