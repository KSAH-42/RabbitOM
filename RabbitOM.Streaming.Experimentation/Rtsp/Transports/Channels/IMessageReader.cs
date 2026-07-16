using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public interface IMessageReader
    {
        byte? PeekValue();

        RtspMessage ReadControlMessage();

        RtspMessage ReadInterleavedMessage();
    }
}
