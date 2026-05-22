using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Models
{
    public interface IStreamElementReader
    {
        IStreamElement ReadElement();
    }
}
