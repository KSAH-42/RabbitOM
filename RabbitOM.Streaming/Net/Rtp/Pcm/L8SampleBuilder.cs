using System;

namespace RabbitOM.Streaming.Net.Rtp.Pcm
{
    public class L8SampleBuilder : RtpSampleBuilder
    {
        protected override void OnPacketAdding( RtpPacketAddingEventArgs e )
        {
            e.CanContinue = e.Packet.Type == RtpPacketType.R8;

            base.OnPacketAdding( e );
        }
    }
}
