using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging.Readers
{
    public interface IStreamReader
    {
        IStreamElement ReadElement();
    }
}
