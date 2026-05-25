using System;
using System.Reflection.Emit;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging.Readers
{
    public sealed class RtspRequestMessageReader
    {
        private readonly IStream _stream;
        
        public RtspRequestMessageReader( IStream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
        }

        public bool CanReadMessage( string startLine )
        {
            throw new NotImplementedException();
        }

        public RtspMessage ReadMessage( string startLine )
        {
            throw new NotImplementedException();
        }
    }
}
