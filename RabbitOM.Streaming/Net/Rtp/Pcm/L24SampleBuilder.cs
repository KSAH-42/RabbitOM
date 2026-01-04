using System;

namespace RabbitOM.Streaming.Net.Rtp.Pcm
{
    public class L24SampleBuilder : RtpSampleBuilder
    {
        protected override void OnPacketAdding( RtpPacketAddingEventArgs e )
        {
            var type = (int) e.Packet.Type;

            e.CanContinue = 96 <= type && type <= 127;

            base.OnPacketAdding( e );
        }
    }
}
