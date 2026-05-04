using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Serialization
{
    public interface IResponseDeserializer
    {
        RtspResponseMessage Deserialize( in ArraySegment<byte> buffer );
    }
}
