using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    public sealed class RtspInterleavedMessageReader : IMessageReader
    {
        private readonly RtspStreamReader _reader;

        public RtspInterleavedMessageReader( IStream stream )
        {
            _reader = new RtspStreamReader( stream );
        }

        public RtspMessage ReadMessage()
        {
            var channel = _reader.ReadByte();

            if ( channel < 0 )
            {
                return null;
            }

            var lengthMsb = _reader.ReadByte();

            if ( lengthMsb < 0 )
            {
                return null;
            }

            var lengthLsb = _reader.ReadByte();

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
                var bytesRead = _reader.Read( buffer , offset , buffer.Length - offset );

                if ( bytesRead <= 0 )
                {
                    return null;
                }

                offset += bytesRead;
            }

            return new RtspInterleavedMessage() { Channel = (byte) ( channel & 0xFF ) , Length = length , Buffer = buffer };
        }
    }
}
