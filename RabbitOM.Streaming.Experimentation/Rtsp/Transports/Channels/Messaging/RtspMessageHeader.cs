using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging
{
    public sealed class RtspMessageHeader : IStreamElement
    {
        public string Name { get; set; }

        public string Value { get; set; }


        public static bool TryParse( string input , out RtspMessageHeader result )
        {
            throw new NotImplementedException();
        }
    }
}
