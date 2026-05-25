using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    // for some reasons, this method doesn't expose a tryParse using a stream because in someway people will think that the toString can be used as well, and a toStream is not a good in my opionion

    public sealed class RtspResponseMessage : RtspMessage
    {
        public string Protocol { get; set; }
        public string Version { get; set; }
        public string Code { get; set; }
        public string Reason { get; set; }
        public RtspHeaderCollection Headers { get; } = new RtspHeaderCollection();
        public byte[] Body { get; set; }
    }
}
