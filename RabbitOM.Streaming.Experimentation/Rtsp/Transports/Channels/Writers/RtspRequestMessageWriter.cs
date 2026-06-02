using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Writers
{
    public sealed class RtspRequestMessageWriter : IMessageWriter<RtspRequestMessage>
    {
        private readonly IStream _stream;

        public RtspRequestMessageWriter( IStream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
        }

        public void WriteMessage( RtspRequestMessage message )
        {
            
        }
    }
}
