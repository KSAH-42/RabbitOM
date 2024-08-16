using System;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    // The following implementation is subject to change or to be removed entirely

    public sealed class H265StreamWriter : IDisposable
    {
        private static readonly byte[] StartPrefix = { 0x00 , 0x00 , 0x00 , 0x01 };

        private readonly RtpMemoryStream _stream = new RtpMemoryStream();



        public long Length
        {
            get => _stream.Length;
        }


        public void Dispose()
        {
            _stream.Dispose();
        }

        public void Clear()
        {
            _stream.Clear();
        }

        public void Write( ArraySegment<byte> buffer )
        {
            if ( buffer.Count == 0 )
            {
                throw new ArgumentException( nameof( buffer ) );
            }

            _stream.WriteAsBinary( StartPrefix );
            _stream.WriteAsBinary( buffer );
        }

        public byte[] ToArray()
        {
            return _stream.ToArray();
        }
    }
}