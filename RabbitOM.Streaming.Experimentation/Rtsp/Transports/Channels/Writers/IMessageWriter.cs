using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Writers
{
    public interface IMessageWriter<in TMessage> where TMessage : RtspMessage
    {
        void WriteMessage( TMessage message );
    }
}
