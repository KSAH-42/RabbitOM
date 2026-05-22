using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Models
{
    public sealed class StreamElementReader : IStreamElementReader
    {
        private readonly Stream _stream;
        private readonly IStreamElementReader _messageReader;
        private readonly IStreamElementReader _interleavedReader;

        public StreamElementReader( Stream stream , IStreamElementReader messageReader , IStreamElementReader interleavedReader )
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
