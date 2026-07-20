using System;

namespace RabbitOM.Streaming.RtspV2.Transports.Channels.Readers
{
    public interface IMessageReader
    {
        byte? PeekValue();

        RtspMessage ReadControlMessage();

        RtspMessage ReadInterleavedMessage();
    }
}
