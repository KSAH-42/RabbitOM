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

            // according to the rfc, length can be equal to zero
            // the following sequence is valid:
            // 0x24 0x00 0x00 0x00 0x24 0x00 0x00 0x01 0x01

            if ( message.Buffer?.Length != message.Buffer.Length )
            {
                throw new ArgumentException( nameof( message ) , "invalid buffer size" );
            }
        }
    }
}
