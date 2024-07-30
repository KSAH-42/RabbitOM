using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265FrameAggregator : IDisposable
    {
        private readonly H265FrameBuilder _builder;

        private readonly RtpPacketQueue _packets;





        public H265FrameAggregator( H265FrameBuilder builder )
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

            if (   packet == null || packet.Payload.Count > _builder.MaximumPayloadSize || _packets.Count > _builder.NumberOfPacketsPerFrame )
            {
                _packets.Clear();

                return false;
            }

            if ( packet.Type != PacketType.MPEG4 ||
                packet.Type != PacketType.MPEG4_DYNAMIC_A ||
                packet.Type != PacketType.MPEG4_DYNAMIC_B 
             )
             {
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
