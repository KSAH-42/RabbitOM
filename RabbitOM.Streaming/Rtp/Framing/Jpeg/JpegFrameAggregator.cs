using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    /// <summary>
    /// Represent a packet aggregator. This class is used to reconstructor a group packet that represent a single frame.
    /// </summary>
    public sealed class JpegFrameAggregator
    {
        private readonly JpegFrameBuilder _builder;

        private readonly RtpPacketAssembler _assembler;





        /// <summary>
        /// Initialize the aggregator
        /// </summary>
        /// <param name="builder">the builder</param>
        /// <exception cref="ArgumentNullException"/>
        public JpegFrameAggregator( JpegFrameBuilder builder )
        {
            _builder   = builder ?? throw new ArgumentNullException( nameof( builder ) );

            _assembler = new RtpPacketAssembler();
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

            if ( OnValidating( packet ) )
            {
                return _assembler.TryAssemble( packet , out result );
            }

            _assembler.Clear();

            return false;
        }

        /// <summary>
        /// Remove all pending packets
        /// </summary>
        public void Clear()
        {
            _assembler.Clear();
        }






        /// <summary>
        /// Occurs to perform before the aggregation
        /// </summary>
        /// <param name="packet">the packet</param>
        /// <returns>returns true for a success, otherwise false</returns>
        private bool OnValidating( RtpPacket packet )
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

            if ( _assembler.Packets.Count > _builder.Configuration.NumberOfPacketsPerFrame )
            {
                return false;
            }

            return true;
        }
    }
}
