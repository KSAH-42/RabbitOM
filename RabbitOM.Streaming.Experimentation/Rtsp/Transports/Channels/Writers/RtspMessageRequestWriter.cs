using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Writers
{
    public sealed class RtspMessageRequestWriter : IMessageWriter<RtspRequestMessage>
    {
        private readonly RtspStreamWriter _writer;
        private readonly IMessageWriterValidator<RtspRequestMessage> _validator;

        public RtspMessageRequestWriter( IStream stream , IMessageWriterValidator<RtspRequestMessage> validator )
        {
            _validator = validator ?? throw new ArgumentNullException( nameof( validator ) );
            _writer = new RtspStreamWriter( stream );
        }

        public void WriteMessage( RtspRequestMessage message )
        {
            _validator.ValidateMessage( message );

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
