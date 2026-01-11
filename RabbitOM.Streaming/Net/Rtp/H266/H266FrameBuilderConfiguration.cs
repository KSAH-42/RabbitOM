using System;

namespace RabbitOM.Streaming.Net.Rtp.H266
{
    public sealed class H266FrameBuilderConfiguration
    {
        public H266FrameBuilderConfiguration( byte[] pps , byte[] sps , byte[] vps )
        {
            PPS = pps;
            SPS = sps;
            VPS = vps;
        }

        public byte[] PPS { get; }

        public byte[] SPS { get; }

        public byte[] VPS { get; }
    }
}
