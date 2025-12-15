using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H265
{
    public sealed class H265Frame : RtpFrame
    {
        public H265Frame( byte[] data , byte[] pps , byte[] sps , byte[] vps , byte[] paramsBuffer ) : base ( data )
        {
            PPS = pps;
            SPS = sps;
            VPS = vps;
            ParamsBuffer = paramsBuffer;
        }

        public byte[] PPS { get; }
        
        public byte[] SPS { get; }

        public byte[] VPS { get; }

        public byte[] ParamsBuffer { get; }
    }
}