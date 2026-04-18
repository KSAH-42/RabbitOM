using System;

namespace RabbitOM.Streaming.Net.Rtp.Pcm
{
    public class L24SampleBuilder : RtpSampleBuilder
    {
        protected override void OnPacketAdding( RtpPacketAddingEventArgs e )
        {
            base.OnPacketAdding( e );

            e.CanContinue &= RtpPacket.IsDynamicType( e.Packet );
        }
    }
}
