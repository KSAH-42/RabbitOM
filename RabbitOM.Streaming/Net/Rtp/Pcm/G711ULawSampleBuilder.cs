using System;

namespace RabbitOM.Streaming.Net.Rtp.Pcm
{
    public class G711ULawSampleBuilder : RtpSampleBuilder
    {
        protected override void OnFilteringPacket( RtpFilteringPacketEventArgs e )
        {
            e.CanContinue = e.Packet.Type == RtpPacketType.PCM_U;

            base.OnFilteringPacket( e );
        }
    }
}
