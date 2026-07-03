using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Writers
{
    public interface IMessageWriterValidator<in TMessage>  where TMessage : RtspMessage
    {
        void ValidateMessage(TMessage message);
    }
}
