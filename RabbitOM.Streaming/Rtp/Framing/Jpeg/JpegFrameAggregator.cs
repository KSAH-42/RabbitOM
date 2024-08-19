using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    /// <summary>
    /// Represent a packet aggregator. This class is used to reconstructor a group packet that represent a single frame.
    /// </summary>
    public sealed class JpegFrameAggregator : IDisposable
    {
        private readonly JpegFrameBuilder _builder;

        private readonly RtpPacketQueue _packets;





        /// <summary>
        /// Initialize the aggregator
        /// </summary>
        /// <param name="builder">the builder</param>
        /// <exception cref="ArgumentNullException"/>
        public JpegFrameAggregator( JpegFrameBuilder builder )
        {
            _builder = builder ?? throw new ArgumentNullException( nameof( builder ) );
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

            if ( ! OnAggregating( packet ) )
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






        /// <summary>
        /// Occurs to perform before the aggregation
        /// </summary>
        /// <param name="packet">the packet</param>
        /// <returns>returns true for a success, otherwise false</returns>
        private bool OnAggregating( RtpPacket packet )
        {
            if ( packet == null )
            {
                return false;
            }

            if ( ! packet.TryValidate() || packet.Type != PacketType.JPEG )
            {
                return false;
            }

            if ( packet.Payload.Count > _builder.Configuration.MaximumPayloadSize )
            {
                return false;
            }

            if ( _packets.Count > _builder.Configuration.NumberOfPacketsPerFrame )
            {
                return false;
            }

            return true;
        }
    }
}
