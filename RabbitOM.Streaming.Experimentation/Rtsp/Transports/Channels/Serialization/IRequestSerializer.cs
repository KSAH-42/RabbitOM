using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Serialization
{
    public interface IRequestSerializer
    {
        byte[] Serialize( RtspRequestMessage message );
    }
}
