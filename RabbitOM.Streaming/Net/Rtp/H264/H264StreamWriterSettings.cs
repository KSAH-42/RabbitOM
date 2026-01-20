using System;

namespace RabbitOM.Streaming.Net.Rtp.H264
{
    public sealed class H264StreamWriterSettings
    {
        public byte[] SPS { get; set; }
        
        public byte[] PPS { get; set; }


        public bool TryValidate()
        {
            return SPS?.Length > 0 && PPS?.Length > 0 ;
        }

        public void Clear()
        {
            SPS = null;
            PPS = null;
        }
    }
}
