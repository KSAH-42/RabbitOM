using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.Jpeg
{
    /// <summary>
    /// Represent a class used for reconstructing a group packets which represent a single frame.
    /// </summary>
    public sealed class JpegFrameAggregator
    {
        private readonly JpegFrameBuilderConfiguration _configuration;

        private readonly RtpPacketAggregator _aggregator;





        /// <summary>
        /// Initialize the aggregator
        /// </summary>
        /// <param name="configuration">the configuration</param>
        /// <exception cref="ArgumentNullException"/>
        public JpegFrameAggregator( JpegFrameBuilderConfiguration configuration )
        {
            _configuration = configuration ?? throw new ArgumentNullException( nameof( configuration ) );

            _aggregator = new RtpPacketAggregator();
        }





        /// <summary>
        /// Try to aggregate until a marker has been detected and depending the builder configuration settings
        /// </summary>
        /// <param name="packet">the packet</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
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

            if ( packet.Type == RtpPacketType.JPEG )
            {
                return _aggregator.TryAggregate( packet , out result );
            }

            return false;
        }

        /// <summary>
        /// Remove all pending packets
        /// </summary>
        public void Clear()
        {
            _aggregator.Clear();
        }
    }
}
