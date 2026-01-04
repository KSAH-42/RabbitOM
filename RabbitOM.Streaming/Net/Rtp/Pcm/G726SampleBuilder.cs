using System;

namespace RabbitOM.Streaming.Net.Rtp.Pcm
{
    public class G726SampleBuilder : RtpSampleBuilder
    {
        protected override void OnPacketAdding( RtpPacketAddingEventArgs e )
        {
            e.CanContinue = e.Packet.Type == RtpPacketType.G726;

            base.OnPacketAdding( e );
        }
    }
}
