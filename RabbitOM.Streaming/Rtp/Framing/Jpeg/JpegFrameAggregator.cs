// This class is used to reassemble packets into one single frame

using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public sealed class JpegFrameAggregator : IDisposable
    {
        private readonly JpegFrameBuilder _builder;
        private readonly RtpPacketQueue _packets;



        public JpegFrameAggregator( JpegFrameBuilder builder )
        {
            _builder = builder ?? throw new ArgumentNullException( nameof( builder ) );
            _packets = new RtpPacketQueue();
        }




        public bool CanAggregate( RtpPacket packet )
        {
            if ( packet == null || packet.Type != PacketType.JPEG )
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

        // no exception throw here, unnecessary to add a try catch block

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
    }
}
