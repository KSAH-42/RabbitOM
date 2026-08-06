using System;

namespace RabbitOM.Streaming.Rtp
{
    public class RtpPacketsLostEventArgs : EventArgs
    {
        public RtpPacketsLostEventArgs( int numberOfPacketLost  )
        {
            if ( numberOfPacketLost < 0 )
            {
                throw new ArgumentException( "invalid count number" , nameof( numberOfPacketLost ) );
            }

            NumberOfPacketLost = numberOfPacketLost;
        }

        public int NumberOfPacketLost
        {
            get;
        }
    }
}
