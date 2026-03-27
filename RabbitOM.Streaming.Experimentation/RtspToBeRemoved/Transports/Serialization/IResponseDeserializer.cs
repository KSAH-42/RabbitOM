using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Transports.Serialization
{
    public interface IResponseDeserializer
    {
        RtspResponseMessage Deserialize( byte[] input );
    }
}
