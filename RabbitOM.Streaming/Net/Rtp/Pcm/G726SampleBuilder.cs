using System;

namespace RabbitOM.Streaming.Net.Rtp.Pcm
{
    public class G726SampleBuilder : RtpSampleBuilder
    {
        public G726SampleBuilder()
        {
        }

        public G726SampleBuilder( G726BitRate bitrate )
        {
            BitRate = bitrate;
        }

        public G726BitRate BitRate { get; } = G726BitRate.Format_16000;

        protected override RtpSample CreateSample( RtpPacket packet )
        {
            return G726Sample.NewSample( packet.Payload.ToArray() , BitRate );
        }

        protected override void OnFilteringPacket( RtpFilteringPacketEventArgs e )
        {
            e.CanContinue = e.Packet.Type == RtpPacketType.G721_726;

            base.OnFilteringPacket( e );
        }
    }
}
