using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging
{
    public sealed class RtspHeader
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public static bool TryParse( string input , out RtspHeader result )
        {
            throw new NotImplementedException();
        }
    }
}
