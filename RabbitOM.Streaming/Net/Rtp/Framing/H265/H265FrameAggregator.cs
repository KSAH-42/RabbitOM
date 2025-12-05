using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.Framing.H265
{
    public sealed class H265FrameAggregator
    {
        private readonly H265FrameBuilder _builder;

        private readonly RtpPacketAggregator _aggregator;





        
        public H265FrameAggregator( H265FrameBuilder builder )
        {
            _builder = builder ?? throw new ArgumentNullException( nameof( builder ) );

            _aggregator = new RtpPacketAggregator();
        }






        public bool TryAggregate( RtpPacket packet , out IEnumerable<RtpPacket> result )
        {
            result = null;

            if ( OnValidating( packet ) )
            {
                return _aggregator.TryAggregate( packet , out result );
            }
            
            return false;
        }

        public void Clear()
        {
            _aggregator.Clear();
        }





        
        private bool OnValidating( RtpPacket packet )
        {
            if ( packet == null )
            {
                return false;
            }

            if ( packet.Payload.Count > _builder.Configuration.MaximumPayloadSize )
            {
                return false;
            }

            if ( _aggregator.Packets.Count > _builder.Configuration.NumberOfPacketsPerFrame )
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
