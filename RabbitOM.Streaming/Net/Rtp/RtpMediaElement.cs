using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public class RtpMediaElement
    {
        public RtpMediaElement( byte[] buffer )
        {
            if ( buffer == null )
            {
                throw new ArgumentNullException( nameof( buffer ) );
            }

            if ( buffer.Length == 0 )
            {
                throw new ArgumentException( nameof( buffer ) );
            }

            Buffer = buffer;
        }

        public byte[] Buffer { get; }
    }
}
