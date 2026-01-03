using System;

namespace RabbitOM.Streaming.Net.Rtp.Pcm
{
    public class L8SampleBuilder : RtpSampleBuilder
    {
        protected override void OnFilteringPacket( RtpFilteringPacketEventArgs e )
        {
            e.CanContinue = e.Packet.Type == RtpPacketType.R8;

            base.OnFilteringPacket( e );
        }
    }
}
