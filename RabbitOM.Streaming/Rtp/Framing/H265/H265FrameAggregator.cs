using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265FrameAggregator
    {
        private readonly H265FrameBuilder _builder;

        private readonly RtpPacketQueue _packets;






        public H265FrameAggregator( H265FrameBuilder builder )
        {
            _builder = builder ?? throw new ArgumentNullException( nameof( builder ) );
            _packets = new RtpPacketQueue();
        }






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
                result = RtpPacketQueue.Sort( _packets );

                _packets.Clear();
            }

            return result != null;
        }

        public void Clear()
        {
            _packets.Clear();
        }









        private bool OnAggregating( RtpPacket packet )
        {
            if ( packet == null )
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

            return packet.Type == PacketType.MPEG4
                || packet.Type == PacketType.MPEG4_DYNAMIC_A
                || packet.Type == PacketType.MPEG4_DYNAMIC_B
                || packet.Type == PacketType.MPEG4_DYNAMIC_C
                || packet.Type == PacketType.MPEG4_DYNAMIC_D
                ;
        }
    }
}
