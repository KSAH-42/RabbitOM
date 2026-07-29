using System;

namespace RabbitOM.Streaming.RtspV2.Transports
{
    public interface IMessageWriter<in TMessage> where TMessage : RtspMessage
    {
        void WriteMessage( TMessage message );
    }
}
