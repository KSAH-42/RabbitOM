using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging.Readers
{
    internal sealed class RtspResponseMessageReader
    {
        private readonly Stream _stream;
        
        public RtspResponseMessageReader( Stream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
        }

        public bool CanReadMessage( string startLine )
        {
            throw new NotImplementedException();
        }

        public RtspRequestMessage ReadMessage( string startLine )
        {
            throw new NotImplementedException();            
        }
    }
}
