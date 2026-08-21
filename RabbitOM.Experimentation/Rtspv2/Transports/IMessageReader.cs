using System;

namespace RabbitOM.Streaming.RtspV2.Transports
{
    public interface IMessageReader
    {
        byte? PeekValue();

        RtspMessage ReadControlMessage();

        RtspMessage ReadInterleavedMessage();
    }
}
