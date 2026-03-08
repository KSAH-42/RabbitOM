using System;

namespace RabbitOM.Streaming.Net.Rtp.H266
{
    public class H266FrameBuilderConfiguration
    {
        public H266FrameBuilderConfiguration( byte[] pps , byte[] sps , byte[] vps , bool donl = false )
        {
            PPS = pps;
            SPS = sps;
            VPS = vps;
            DONL = donl;
        }

        public byte[] PPS { get; }

        public byte[] SPS { get; }

        public byte[] VPS { get; }

        public bool DONL { get; set; }
    }
}
