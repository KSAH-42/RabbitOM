using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    internal static class RtpStartCodePrefix
    {
        public static readonly byte[] Default = new byte[] { 0x00 , 0x00 , 0x00 , 0x01 };
    }
}
