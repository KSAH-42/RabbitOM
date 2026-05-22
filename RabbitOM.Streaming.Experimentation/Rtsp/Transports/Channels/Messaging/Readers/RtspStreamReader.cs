using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging.Readers
{
    public sealed class RtspStreamReader : IStreamReader
    {
        private readonly Stream _stream;

        private readonly RtspMessageReader _messageReader;

        private readonly RtspInterleavedDataReader _interleavedReader;


        public RtspStreamReader( Stream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );

            _messageReader = new RtspMessageReader( stream );

            _interleavedReader = new RtspInterleavedDataReader( stream );
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
            
            return _messageReader.ReadElement( (char) character );
        }
    }
}
