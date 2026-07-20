using System;

namespace RabbitOM.Sample.Client.H264.Codecs
{
    public struct H264Surface
    {
        public H264Surface( byte[] startCodePrefix , byte[] pps , byte[] sps )
        {
            StartCodePrefix = startCodePrefix;
            PPS = pps;
            SPS = sps;
        }

        public byte[] StartCodePrefix { get; }

        public byte[] PPS { get; }

        public byte[] SPS { get; }
    }
}