using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public sealed class RtspInterleavedMessageValidator
    {
        public void ValidateMessage( RtspInterleavedMessage message )
        {
            if ( message == null )
            {
                throw new ArgumentNullException( nameof( message ) );
            }

            // according to the rfc, the length field can be equal to zero
            // the following sequence is also valid:

            // 0x24 0x00 0x00 0x00
            // 0x24 0x00 0x00 0x01 0x01
            // 0x24 0x00 0x00 0x02 0x01 0x10

            var bufferLength = message.Buffer?.Length ?? 0;

            if ( bufferLength != message.Length )
            {
                throw new ArgumentException( nameof( message ) , "invalid buffer size" );
            }
        }
    }
}
