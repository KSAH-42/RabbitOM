using System;

namespace RabbitOM.Streaming.Net.Rtp.Pcm
{
    public class L16SampleBuilder : RtpSampleBuilder
    {
        protected override void OnPacketAdding( RtpPacketAddingEventArgs e )
        {
            e.CanContinue = e.Packet.Type == RtpPacketType.L16_A || e.Packet.Type == RtpPacketType.L16_B;

            base.OnPacketAdding( e );
        }
    }
}
