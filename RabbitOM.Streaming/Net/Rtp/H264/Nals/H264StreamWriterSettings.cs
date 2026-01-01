using System;

namespace RabbitOM.Streaming.Net.Rtp.H264.Nals
{
    public sealed class H264StreamWriterSettings
    {
        public byte[] PPS { get; set; }

        public byte[] SPS { get; set; }
        

        public bool TryValidate()
        {
            return PPS?.Length > 0 && SPS?.Length > 0;
        }

        public void Clear()
        {
            PPS = null;
            SPS = null;
        }
    }
}
