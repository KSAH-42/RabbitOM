using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public class MediaElement
    {
        public MediaElement( byte[] buffer )
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

        public DateTime TimeStamp { get; } = DateTime.Now;

        public byte[] Buffer { get; }
    }
}
