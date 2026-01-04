using System;

namespace RabbitOM.Streaming.Net.Rtp.Pcm
{
    public class G726SampleMediaElement : RtpMediaElement
    {
        public G726SampleMediaElement( G726BitRate bitrate , byte[] data ) : base( data )
        {
            BitRate = bitrate;
        }

        public G726BitRate BitRate { get; }
    }
}