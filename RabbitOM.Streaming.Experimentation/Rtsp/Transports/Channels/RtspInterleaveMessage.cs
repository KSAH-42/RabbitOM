using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    // for some reasons, this method doesn't expose a tryParse using a stream because in someway people will think that the toString can be used as well, but a toStream or a toArray method is not a good in my opinion for this case

    public sealed class RtspInterleaveMessage : RtspMessage
    {
        public int Channel { get; set; }

        public ushort Length { get; set; }

        public byte[] Buffer { get; set; }
    }
}
