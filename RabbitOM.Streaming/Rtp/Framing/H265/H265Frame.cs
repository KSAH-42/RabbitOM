using System;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265Frame : RtpFrame
    {
        public H265Frame( byte[] data , byte[] vps , byte[] sps , byte[] pps ) 
            : base ( data )
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