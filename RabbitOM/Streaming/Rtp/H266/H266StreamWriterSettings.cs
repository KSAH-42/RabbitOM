using System;

namespace RabbitOM.Streaming.Rtp.H266
{
    public sealed class H266StreamWriterSettings
    {
        public bool DONL { get; set; }

        public byte[] PPS { get; set; }

        public byte[] SPS { get; set; }

        public byte[] VPS { get; set; }





        public bool TryValidate()
        {
            return VPS?.Length > 0 && SPS?.Length > 0 && PPS?.Length > 0 ;
        }

        public void Clear()
        {
            DONL = false;
            PPS = null;
            SPS = null;
            VPS = null;
        }
    }
}
