using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging.Readers
{
    public sealed class RtspMessageReader : IMessageReader
    {
        private readonly Stream _stream;

        private readonly RtspInterleaveMessageReader _interleaveMessageReader;

        private readonly RtspRequestMessageReader _requestMessageReader;

        private readonly RtspResponseMessageReader _responseMessageReader;
        



        public RtspMessageReader( Stream stream )
        {
            if ( stream == null )
            {
                throw new ArgumentNullException( nameof( stream ) );
            }

            _stream = stream;
            _interleaveMessageReader = new RtspInterleaveMessageReader( _stream );
            _requestMessageReader = new RtspRequestMessageReader( _stream );
            _responseMessageReader = new RtspResponseMessageReader( _stream );
        }



        public RtspMessage ReadMessage()
        {
            var prefix = _stream.ReadByte();

            if ( prefix <= 0 )
            {
                return null;
            }

            if ( RtspInterleaveMessage.IsInterleaveMessage( (char) prefix ) )
            {
                return _interleaveMessageReader.ReadMessage();
            }

            var startLine = _stream.ReadLine();

            if ( RtspResponseMessage.IsResponseMessage( startLine ) )
            {
                return _responseMessageReader.ReadMessage( startLine );
            }

            if ( RtspRequestMessage.IsRequestMessage( startLine ) )
            {
                return _requestMessageReader.ReadMessage( startLine );
            }

            throw new InvalidDataException();
        }
    }
}
