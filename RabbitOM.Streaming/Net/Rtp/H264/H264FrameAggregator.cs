using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H264
{
    public sealed class H264FrameAggregator
    {
        private readonly H264FrameBuilderConfiguration _configuration;

        private readonly RtpPacketAggregator _aggregator;





        
        public H264FrameAggregator( H264FrameBuilderConfiguration configuration )
        {
            _configuration = configuration ?? throw new ArgumentNullException( nameof( configuration ) );

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

            if ( packet.Payload.Count > _configuration.MaximumPayloadSize )
            {
                return false;
            }

            if ( _aggregator.Packets.Count > _configuration.NumberOfPacketsPerFrame )
            {
                return false;
            }

            return packet.Type == PacketType.MPEG4
                || packet.Type == PacketType.MPEG4_DYNAMIC_A
                ;
        }
    }
}
