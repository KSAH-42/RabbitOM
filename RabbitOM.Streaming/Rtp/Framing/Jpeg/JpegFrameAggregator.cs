// This class is used to reassemble packets into one single frame

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

            if ( ! CanAggregate( packet ) )
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

        private bool CanAggregate( RtpPacket packet )
        {
            if ( packet == null || packet.Type != PacketType.JPEG )
            {
                return false;
            }

            if ( packet.Payload.Count > _configuration.MaximumPayloadSize )
            {
                return false;
            }

            if ( _packets.Count > _configuration.NumberOfPacketsPerFrame )
            {
                return false;
            }

            return true;
        }
    }
}
