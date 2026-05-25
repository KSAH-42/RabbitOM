using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    public sealed class RtspRequestResponseMessageReader
    {
        private readonly IStream _stream;
        
        public RtspRequestResponseMessageReader( IStream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
        }

        public RtspMessage ReadMessage( string startLine )
        {
            throw new NotImplementedException();
        }
    }
}
