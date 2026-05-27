using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public sealed class RtspRequestLine
    {
        public string Method { get; set; }

        public string Uri { get; set; }

        public string Protocol { get; set; }

        public string Version { get; set; }

        public static bool TryParse( string input , out RtspRequestLine result )
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
