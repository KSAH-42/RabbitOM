using System;

namespace RabbitOM.Net.RtspV2.Transports
{
    public interface IMessageReader
    {
        byte? PeekValue();

        RtspMessage ReadControlMessage();

        RtspMessage ReadInterleavedMessage();
    }
}
