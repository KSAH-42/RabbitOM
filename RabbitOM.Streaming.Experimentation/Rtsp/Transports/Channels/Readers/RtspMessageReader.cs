using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    public sealed class RtspMessageReader : IMessageReader
    {
        private readonly InternalRtspMessageReader _reader;

        public RtspMessageReader( IStream stream )
        {
            _reader = new InternalRtspMessageReader( stream );
        }

        public RtspMessage ReadMessage()
        {
            var prefix = _reader.PeekValue();

            if ( prefix < 0 )
            {
                return null;
            }

            if ( prefix == '$' )
            {
                return _reader.ReadInterleavedMessage();
            }

            return _reader.ReadMessage();
        }
    }
}
