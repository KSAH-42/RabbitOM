using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Transports.Serialization
{
    public interface IRequestSerializer
    {
        byte[] Serialize( RtspRequestMessage message );
    }
}
