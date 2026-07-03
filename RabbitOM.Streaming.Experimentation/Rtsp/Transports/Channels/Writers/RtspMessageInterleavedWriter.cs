using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Writers
{
    public sealed class RtspMessageInterleavedWriter : IMessageWriter<RtspInterleavedMessage>
    {
        private readonly RtspStreamWriter _writer;
        private readonly IMessageWriterValidator<RtspInterleavedMessage> _validator;

        public RtspMessageInterleavedWriter( IStream stream , IMessageWriterValidator<RtspInterleavedMessage> validator )
        {
            _validator = validator ?? throw new ArgumentNullException( nameof( validator ) );
            _writer = new RtspStreamWriter( stream );
        }

        public void WriteMessage( RtspInterleavedMessage message )
        {
            _validator.ValidateMessage( message );

            _writer.WriteChar( '$' );
            _writer.WriteByte( message.Channel );
            _writer.WriteByte( (byte) (message.Length >> 8 & 0xFF) );
            _writer.WriteByte( (byte) (message.Length      & 0xFF) );
            _writer.Write( message.Buffer );
            _writer.Flush();
        }
    }
}
