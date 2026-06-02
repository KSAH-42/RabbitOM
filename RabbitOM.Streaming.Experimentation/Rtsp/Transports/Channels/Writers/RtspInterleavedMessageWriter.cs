using System;
using System.Diagnostics;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Writers
{
    public sealed class RtspInterleavedMessageWriter : IMessageWriter<RtspInterleavedMessage>
    {
        private readonly IStream _stream;

        public RtspInterleavedMessageWriter( IStream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
        }

        public void WriteMessage( RtspInterleavedMessage message )
        {
            throw new NotImplementedException();
        }
    }
}
