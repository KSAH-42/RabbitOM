using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    public interface IMessageReader
    {
        byte? Peek();

        RtspMessage ReadControlMessage();

        RtspInterleavedMessage ReadInterleavedMessage();
    }
}
