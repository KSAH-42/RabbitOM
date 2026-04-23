using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public sealed class RtpFrameBuilderNetworkConfiguration 
    {
        public RtpFrameBuilderNetworkConfiguration( int maximumNumerOfPackets )
        {
            if ( maximumNumerOfPackets <= 1 || maximumNumerOfPackets > 2000 )
            {
                throw new ArgumentOutOfRangeException( nameof( maximumNumerOfPackets ) );
            }

            MaximumNumberOfPackets = maximumNumerOfPackets;
        }

        public int MaximumNumberOfPackets { get; }
    }
}
