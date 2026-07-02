// don't remove the statement for flushing data, it's mandatory by desin that's one of the core concept
using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Writers
{
    public sealed class RtspMessageWriter : IMessageWriter<RtspInterleavedMessage>, IMessageWriter<RtspRequestMessage>
    {
        private readonly RtspStreamWriter _writer;

        public RtspMessageWriter( IStream stream )
        {
            _writer = new RtspStreamWriter( stream );
        }

        public void WriteMessage( RtspInterleavedMessage message )
        {
            RtspMessageWriterValidator.ValidateMessage( message );

            _writer.WriteChar( '$' );
            _writer.WriteByte( message.Channel );
            _writer.WriteByte( (byte) (message.Length >> 8 & 0xFF) );
            _writer.WriteByte( (byte) (message.Length      & 0xFF) );
            _writer.Write( message.Buffer );
            _writer.Flush();
        }

        public void WriteMessage( RtspRequestMessage message )
        {
            RtspMessageWriterValidator.ValidateMessage( message );

            _writer.WriteLine( message.RequestLine.ToString() );

            foreach ( var header in message.Headers )
            {
                _writer.WriteLine( "{0}: {1}" , header.Key , header.Value );
            }

            _writer.WriteLine();
            _writer.Write( message.Body );
            _writer.Flush();
        }
    }
}
