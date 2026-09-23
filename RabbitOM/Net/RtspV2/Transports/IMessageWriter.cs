using System;

namespace RabbitOM.Net.RtspV2.Transports
{
    public interface IMessageWriter<in TMessage> where TMessage : RtspMessage
    {
        void WriteMessage( TMessage message );
    }
}
