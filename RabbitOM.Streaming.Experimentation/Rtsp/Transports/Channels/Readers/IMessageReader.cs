using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    public interface IMessageReader
    {
        byte? PeekValue();

        RtspMessage ReadControlMessage();

        RtspInterleavedMessage ReadInterleavedMessage();
    }
}
