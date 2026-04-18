using System;

namespace RabbitOM.Streaming.Net.Rtp.Pcm
{
    public class L8SampleBuilder : RtpSampleBuilder
    {
        protected override void OnPacketAdding( RtpPacketAddingEventArgs e )
        {
            base.OnPacketAdding( e );

            e.CanContinue &= e.Packet.Type == RtpPacketType.CN;
        }
    }
}
