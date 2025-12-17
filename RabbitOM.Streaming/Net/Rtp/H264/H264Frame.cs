using System;

namespace RabbitOM.Streaming.Net.Rtp.H264
{
    public sealed class H264Frame : RtpFrame
    {
        public H264Frame( byte[] data , byte[] pps , byte[] sps , byte[] paramsBuffer ) : base ( data )
        {
            PPS = pps;
            SPS = sps;
            ParamsBuffer = paramsBuffer;
        }

        public byte[] PPS { get; }
        
        public byte[] SPS { get; }

        public byte[] ParamsBuffer { get; }
    }
}