using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public sealed class JpegFrameAggregator : IDisposable
    {
        private readonly JpegFrameBuilderConfiguration _configuration;
        private readonly RtpPacketQueue _packets;



        public JpegFrameAggregator( JpegFrameBuilderConfiguration configuration )
        {
            _configuration = configuration ?? throw new ArgumentNullException( nameof( configuration ) );
            _packets = new RtpPacketQueue();
        }




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

        public void Clear()
        {
            _packets.Clear();
        }

        public void Dispose()
        {
            _packets.Clear();
        }
    }
}
