using System;

namespace RabbitOM.Streaming.Net.Rtp.Pcm
{
    public class G726SampleBuilder : RtpSampleBuilder
    {
        public G726SampleBuilder() { }

        public G726SampleBuilder( G726BitRate bitrate ) 
            => BitRate = bitrate;

        public G726BitRate BitRate { get; }

        protected override RtpMediaElement CreateMediaSample( RtpPacket packet ) 
            => G726SampleMediaElement.NewSample( packet.Payload.ToArray() , BitRate );

        protected override void OnPacketAdding( RtpPacketAddingEventArgs e )
        {
            e.CanContinue = e.Packet.Type == RtpPacketType.G726;

            base.OnPacketAdding( e );
        }
    }
}
