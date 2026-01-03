using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public sealed class DefaultRtpPacketInspector : RtpPacketInspector
    {
        public const int DefaultMTU = 1500;

        public const int DefaultMaximumOfPacketsSize = DefaultMTU * 4;

        public bool DetectLargePayload { get; set; } = true;

        public int MaximumPayloadSize { get; set; } = DefaultMaximumOfPacketsSize;

        public override void Inspect( RtpPacket packet )
        {
            if ( ! TryInspect( packet ) )
            {
                throw new InvalidOperationException();
            }
        }

        public override bool TryInspect( RtpPacket packet )
        {
            if ( packet == null || ! packet.TryValidate() )
            {
                return false;
            }

            if ( DetectLargePayload && packet.Payload.Count > MaximumPayloadSize )
            {
                return false;
            }

            return true;
        }
    }
}
