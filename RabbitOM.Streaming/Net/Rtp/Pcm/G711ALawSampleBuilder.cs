using System;

namespace RabbitOM.Streaming.Net.Rtp.Pcm
{
    public class G711ALawSampleBuilder : RtpSampleBuilder
    {
        protected override void OnFilteringPacket( RtpFilteringPacketEventArgs e )
        {
            e.CanContinue = e.Packet.Type == RtpPacketType.PCM_A;

            base.OnFilteringPacket( e );
        }
    }
}
