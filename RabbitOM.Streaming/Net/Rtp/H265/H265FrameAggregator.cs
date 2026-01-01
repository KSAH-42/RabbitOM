using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H265
{
    /// <summary>
    /// Represente a H265 frame aggregator
    /// </summary>
    public sealed class H265FrameAggregator
    {
        private readonly H265FrameBuilderConfiguration _configuration;

        private readonly RtpPacketAggregator _aggregator;





        /// <summary>
        /// Initialize a new instance of the frame aggregator
        /// </summary>
        /// <param name="configuration">the configuration object</param>
        /// <exception cref="ArgumentNullException"/>
        public H265FrameAggregator( H265FrameBuilderConfiguration configuration )
        {
            _configuration = configuration ?? throw new ArgumentNullException( nameof( configuration ) );

            _aggregator = new RtpPacketAggregator();
        }





        /// <summary>
        /// Try to aggregate rtp packet
        /// </summary>
        /// <param name="packet">the packet</param>
        /// <param name="result">the collection of aggregated packet after succeed</param>
        /// <returns>returns true for a success otherwise false</returns>
        public bool TryAggregate( RtpPacket packet , out IEnumerable<RtpPacket> result )
        {
            result = null;

            if ( packet == null || ! packet.TryValidate() )
            {
                return false;
            }

            if ( packet.Payload.Count > _configuration.MaximumPayloadSize )
            {
                return false;
            }

            if ( _aggregator.Packets.Count > _configuration.NumberOfPacketsPerFrame )
            {
                return false;
            }

            if (   packet.Type == RtpPacketType.MPEG4
                || packet.Type == RtpPacketType.MPEG4_DYNAMIC_A
                || packet.Type == RtpPacketType.MPEG4_DYNAMIC_B
                || packet.Type == RtpPacketType.MPEG4_DYNAMIC_C
                || packet.Type == RtpPacketType.MPEG4_DYNAMIC_D
                )
            {
                return _aggregator.TryAggregate( packet , out result );
            }
            
            return false;
        }

        /// <summary>
        /// Clear all remaining packets
        /// </summary>
        public void Clear()
        {
            _aggregator.Clear();
        }
    }
}
