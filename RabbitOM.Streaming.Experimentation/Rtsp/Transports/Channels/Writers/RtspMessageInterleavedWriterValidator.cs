using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Writers
{
    public sealed class RtspMessageInterleavedWriterValidator : IMessageWriterValidator<RtspInterleavedMessage>
    {
        public void ValidateMessage( RtspInterleavedMessage message )
        {
            if ( message == null )
            {
                throw new ArgumentNullException( nameof( message ) );
            }

            var bufferLength = message.Buffer?.Length ?? 0; // according to the rfc, the length field can be equal to zero

            if ( bufferLength != message.Length )
            {
                throw new ArgumentException( "invalid buffer size" , nameof( message ) );
            }
        }
    }
}
