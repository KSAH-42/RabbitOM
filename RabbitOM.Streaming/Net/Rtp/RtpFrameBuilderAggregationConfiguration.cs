using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public sealed class RtpFrameBuilderAggregationConfiguration 
    {
        public RtpFrameBuilderAggregationConfiguration( int maximumNumerOfPackets )
        {
            if ( maximumNumerOfPackets <= 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( maximumNumerOfPackets ) );
            }

            MaximumNumberOfPackets = maximumNumerOfPackets;
        }

        public int MaximumNumberOfPackets { get; }
    }
}
