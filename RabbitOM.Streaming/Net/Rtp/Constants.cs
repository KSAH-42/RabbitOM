using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public static class Constants
    {
        public const int DefaultMTU = 1500;

        public const int DefaultMaximumOfPackets = 5000;

        public const int DefaultMaximumOfPacketsSize = DefaultMTU * 4;
    }
}
