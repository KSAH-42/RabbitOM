using System;

namespace RabbitOM.Streaming.RtspV2.Transports.Channels.Readers
{
    public sealed class RtspMessageReaderService
    {
        private readonly IMessageReader _reader;

        public RtspMessageReaderService( IMessageReader reader )
        {
            _reader = reader ?? throw new ArgumentNullException( nameof( reader ) );
        }

        public RtspMessage ReadMessage()
        {
            var prefix = _reader.PeekValue();

            if ( ! prefix.HasValue )
            {
                return null;
            }

            if ( prefix.Value == '$' )
            {
                return _reader.ReadInterleavedMessage();
            }

            return _reader.ReadControlMessage();
        }
    }
}
