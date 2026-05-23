using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging
{
    public sealed class RtspInterleaveMessage : RtspMessage
    {
        public int Channel { get; set; }
        public byte[] Buffer { get; set; }

        public static bool IsInterleaveMessage( char value )
        {
            return value == '$';
        }
    }
}
