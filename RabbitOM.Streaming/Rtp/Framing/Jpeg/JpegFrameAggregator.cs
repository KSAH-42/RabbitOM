using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    /// <summary>
    /// Represent a packet aggregator. This class is used to reconstructor a group packet that represent a single frame.
    /// </summary>
    public sealed class JpegFrameAggregator : IDisposable
    {
        private readonly JpegFrameBuilderConfiguration _configuration;

        private readonly RtpPacketQueue _packets;





        /// <summary>
        /// Initialize the aggregator
        /// </summary>
        /// <param name="configuration">the configuration</param>
        /// <exception cref="ArgumentNullException"/>
        public JpegFrameAggregator( JpegFrameBuilderConfiguration configuration )
        {
            _configuration = configuration ?? throw new ArgumentNullException( nameof( configuration ) );
            _packets = new RtpPacketQueue();
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

            if ( packet == null || packet.Type != PacketType.JPEG  || packet.Payload.Count > _configuration.MaximumPayloadSize || _packets.Count > _configuration.NumberOfPacketsPerFrame )
            {
                _packets.Clear();

                return false;
            }

            _packets.Enqueue( packet );

            if ( packet.Marker )
            {
                result = _packets.ToArray();

                _packets.Clear();
            }

            return result != null;
        }

        /// <summary>
        /// Remove all pending packets
        /// </summary>
        public void Clear()
        {
            _packets.Clear();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _packets.Clear();
        }
    }
}
