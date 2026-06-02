using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Writers
{
    public interface IMessageWriter<TMessage>
        where TMessage : RtspMessage
    {
        void WriteMessage( TMessage message );
    }
}
