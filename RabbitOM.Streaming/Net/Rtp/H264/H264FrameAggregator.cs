using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H264
{
    /// <summary>
    /// Represent a H264 frame aggregator
    /// </summary>
    public sealed class H264FrameAggregator
    {
        private readonly H264FrameBuilderConfiguration _configuration;

        private readonly RtpPacketAggregator _aggregator;





        
        /// <summary>
        /// Initialize a new instance of the a H264 frame aggregatpr
        /// </summary>
        /// <param name="configuration">the configuration</param>
        /// <exception cref="ArgumentNullException"></exception>
        public H264FrameAggregator( H264FrameBuilderConfiguration configuration )
        {
            _configuration = configuration ?? throw new ArgumentNullException( nameof( configuration ) );

            _aggregator = new RtpPacketAggregator();
        }








        /// <summary>
        /// Try to aggregate packets
        /// </summary>
        /// <param name="packet">the packet</param>
        /// <param name="result">the aggregated packets if it succeed</param>
        /// <returns>returns true for a success, otherwise false</returns>
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

            if ( packet.Type == RtpPacketType.MPEG4 || packet.Type == RtpPacketType.MPEG4_DYNAMIC_A )
            {
                return _aggregator.TryAggregate( packet , out result );
            }

            return false;
        }

        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            _aggregator.Clear();
        }
    }
}
