using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Writers
{
    public interface IMessageWriter
    {
        void WriteMessage( RtspMessage message );
    }
}
