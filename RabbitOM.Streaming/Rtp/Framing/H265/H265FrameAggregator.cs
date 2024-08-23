using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265FrameAggregator
    {
        private readonly H265FrameBuilder _builder;

        private readonly RtpPacketAssembler _assembler;





        
        public H265FrameAggregator( H265FrameBuilder builder )
        {
            _builder = builder ?? throw new ArgumentNullException( nameof( builder ) );

            _assembler = new RtpPacketAssembler();
        }






        public bool TryAggregate( RtpPacket packet , out IEnumerable<RtpPacket> result )
        {
            result = null;

            if ( OnAggregating( packet ) )
            {
                return _assembler.TryAssemble( packet , out result );
            }

            _assembler.Clear();
            
            return false;
        }

        public void Clear()
        {
            _assembler.Clear();
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

            if ( _assembler.Packets.Count > _builder.Configuration.NumberOfPacketsPerFrame )
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
