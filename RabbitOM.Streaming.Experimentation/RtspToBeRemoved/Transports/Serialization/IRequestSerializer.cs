using System;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Transports.Serialization
{
    public interface IRequestSerializer
    {
        byte[] Serialize( RtspRequestMessage message );
    }
}
