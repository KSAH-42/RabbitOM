using System;
using System.CodeDom;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging.Readers
{
    internal sealed class RtspInterleaveMessageReader
    {
        private readonly Stream _stream;
        
        public RtspInterleaveMessageReader( Stream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
        }

        public RtspInterleaveMessage ReadMessage()
        {
            var channel = _stream.ReadByte();

            if ( channel < 0 )
            {
                return null;
            }

            var sizeMsb = _stream.ReadByte();

            if ( sizeMsb < 0 )
            {
                return null;
            }

            var sizeLsb = _stream.ReadByte();

            if ( sizeLsb < 0 )
            {
                return null;
            }

            var size = sizeMsb << 8  + sizeLsb;

            if ( size <= 0 )
            {
                return null;
            }

            var buffer = new byte[ size ];
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

            return new RtspInterleaveMessage() { Channel = channel , Buffer = buffer };
        }
    }
}
