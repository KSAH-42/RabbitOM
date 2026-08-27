using System;

namespace RabbitOM.Net.Rtp.Pcm
{
    public class G711ULawSampleBuilder : RtpSampleBuilder
    {
        protected override void OnPacketAdding( RtpPacketAddingEventArgs e )
        {
            base.OnPacketAdding( e );

            e.CanContinue &= e.Packet.Type == RtpPacketType.PCM_U;
        }
    }
}
