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
            Clear();
        }






        private bool CanAggregate( RtpPacket packet )
        {
            if ( packet == null || packet.Type != 26 )
            {
                return false;
            }

            if ( packet.Payload.Count > _builder.Configuration.PayloadMaxSize )
            {
                return false;
            }

            if ( _packets.Count > _builder.Configuration.PayloadMaxSize )
            {
                return false;
            }

            return true;
        }
    }
}
