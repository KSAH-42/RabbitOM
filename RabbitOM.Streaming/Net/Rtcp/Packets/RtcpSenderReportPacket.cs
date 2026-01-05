using System;

namespace RabbitOM.Streaming.Net.Rtcp.Packets
{
    public struct RtcpSenderReportPacket
    {
        public ulong NtpTimeStamp { get; private set; }

        public uint RtpTimeStamp { get; private set; }

        public uint PacketCount { get; private set; }

        public uint BytesSend { get; private set; }


        public static bool TryParse( in ArraySegment<byte> payload , out RtcpSenderReportPacket result )
        {
            throw new NotImplementedException();
        }
    }
}
