using System;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Transports.Serialization
{
    public interface IResponseDeserializer
    {
        RtspResponseMessage Deserialize( byte[] input );
    }
}
