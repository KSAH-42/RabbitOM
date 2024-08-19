using System;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265Frame : RtpFrame
    {
        public H265Frame( byte[] data , byte[] vps , byte[] sps , byte[] pps ) 
            : base ( data )
        {
            VPS = vps;
            SPS = sps;
            PPS = pps;
        }

        public byte[] VPS { get; }
        public byte[] SPS { get; }
        public byte[] PPS { get; }
    }
}