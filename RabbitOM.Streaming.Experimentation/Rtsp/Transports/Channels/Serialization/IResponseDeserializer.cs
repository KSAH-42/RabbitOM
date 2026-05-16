using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Serialization
{
    public interface IResponseDeserializer
    {
        RtspResponseMessage Deserialize( byte[] buffer );
    }
}
