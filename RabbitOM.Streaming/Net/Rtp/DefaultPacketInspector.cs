using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    // TODO: adding the detection of large sequence
    // TODO: adding rtp stats, not necessary the bandwith it should be done elsewehere
    // TODO: adding event handler for triggering inspection reports ?
    public sealed class DefaultPacketInspector : RtpPacketInspector
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
                throw new ArgumentException( nameof( packet ) , "the packet seems to be incorrect" );
            }

            if ( DetectLargePayload && packet.Payload.Count > MaximumPayloadSize )
            {
                throw new InvalidOperationException( "UnAuthorize payload size" );
            }
        }
    }
}
