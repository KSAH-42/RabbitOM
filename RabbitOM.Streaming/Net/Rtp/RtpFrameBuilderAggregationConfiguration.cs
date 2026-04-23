using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public sealed class RtpFrameBuilderAggregationConfiguration 
    {
        public const int DefaultSequenceMaxSize = 4000;

        public RtpFrameBuilderAggregationConfiguration() : this ( DefaultSequenceMaxSize )
        {
        }

        public RtpFrameBuilderAggregationConfiguration( int maximumNumerOfPackets )
        {
            if ( maximumNumerOfPackets <= 1 || maximumNumerOfPackets > DefaultSequenceMaxSize )
            {
                throw new ArgumentOutOfRangeException( nameof( maximumNumerOfPackets ) );
            }

            MaximumNumberOfPackets = maximumNumerOfPackets;
        }

        public int MaximumNumberOfPackets { get; }
    }
}
