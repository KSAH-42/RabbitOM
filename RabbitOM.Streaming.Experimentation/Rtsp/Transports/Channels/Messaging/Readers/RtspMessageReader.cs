using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging.Readers
{
    public sealed class RtspMessageReader : IMessageReader
    {
        private readonly RtspStream _stream;
        private readonly IMessageReader _communicationReader;
        private readonly IMessageReader _interleavedReader;        


        public RtspMessageReader( RtspStream stream , IMessageReader communicationReader , IMessageReader interleaveReader )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );

            _communicationReader = communicationReader ?? throw new ArgumentNullException( nameof( communicationReader ) );

            _interleavedReader = interleaveReader ?? throw new ArgumentNullException( nameof( interleaveReader ) );
        }


        public RtspMessage ReadMessage()
        {
            var prefix = _stream.PeekByte();

            if ( prefix <= 0 )
            {
                return null;
            }

            if ( prefix == '$' )
            {
                return _interleavedReader.ReadMessage();
            }
            
            return _communicationReader.ReadMessage();
        }
    }
}
