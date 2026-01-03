using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    // TODO: adding the detection of large sequence
    public sealed class DefaultRtpPacketInspector : RtpPacketInspector
    {
        public const int DefaultMTU = 1500;

        public const int DefaultMaximumOfPacketsSize = DefaultMTU * 4;

        public bool DetectLargePayload { get; set; } = true;

        public int MaximumPayloadSize { get; set; } = DefaultMaximumOfPacketsSize;

        public override void Inspect( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( ! packet.TryValidate() )
            {
                throw new ArgumentException( nameof( packet ) , "the packet seems incorrect" );
            }

            if ( DetectLargePayload && packet.Payload.Count > MaximumPayloadSize )
            {
                throw new InvalidOperationException( "UnAuthorize payload size" );
            }
        }
    }
}
