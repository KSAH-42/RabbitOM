using System;

namespace RabbitOM.Streaming.RtspV2.Transports.Channels.Writers
{
    public interface IMessageWriter<in TMessage> where TMessage : RtspMessage
    {
        void WriteMessage( TMessage message );
    }
}
