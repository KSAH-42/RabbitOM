using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public sealed class RtspStatusLine
    {
        public string Protocol { get; set; }

        public string Version { get; set; }

        public string Code { get; set; }

        public string Reason { get; set; }

        public static bool TryParse( string input , out RtspStatusLine result )
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
