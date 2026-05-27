using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    public sealed class RtspInterleaveMessageReader
    {
        private readonly IStream _stream;
        
        public RtspInterleaveMessageReader( IStream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
        }

        public RtspMessage ReadMessage()
        {
            var magicByte = _stream.ReadByte();

            if ( magicByte != '$' )
            {
                return null;
            }

            var channel = _stream.ReadByte();

            if ( channel < 0 )
            {
                return null;
            }

            var lengthMsb = _stream.ReadByte();

            if ( lengthMsb < 0 )
            {
                return null;
            }

            var lengthLsb = _stream.ReadByte();

            if ( lengthLsb < 0 )
            {
                return null;
            }

            var length = (ushort) ( (lengthMsb << 8  + lengthLsb) & 0xFFFF );

            if ( length <= 0 )
            {
                return null;
            }

            var buffer = new byte[ length ];
            var offset = 0;

            while ( offset < buffer.Length )
            {
                var bytesRead = _stream.Read( buffer , offset , buffer.Length - offset );

                if ( bytesRead <= 0 )
                {
                    return null;
                }

                offset += bytesRead;
            }

            return new RtspInterleaveMessage() { Channel = channel , Length = length , Buffer = buffer };
        }
    }
}
