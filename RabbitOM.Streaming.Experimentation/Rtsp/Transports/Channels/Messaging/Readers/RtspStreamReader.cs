using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging.Readers
{
    public sealed class RtspStreamReader : IStreamReader
    {
        private readonly Stream _stream;

        private readonly IStreamReader _messageReader;

        private readonly IStreamReader _interleavedReader;


        public RtspStreamReader( Stream stream , IStreamReader messageReader , IStreamReader interleavedReader )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );

            _messageReader = messageReader ?? throw new ArgumentNullException( nameof( messageReader ) );

            _interleavedReader = interleavedReader ?? throw new ArgumentNullException( nameof( interleavedReader ) );
        } 


        public IStreamElement ReadElement()
        {
            var character = _stream.ReadByte();

            if ( character <= 0)
            {
                return null;
            }

            else if ( character == '$' )
            {
                return _interleavedReader.ReadElement();
            }
            
            return _messageReader.ReadElement();
        }
    }
}
