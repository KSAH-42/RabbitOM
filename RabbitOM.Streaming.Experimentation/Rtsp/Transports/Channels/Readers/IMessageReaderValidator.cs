using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    public interface IMessageReaderValidator
    {
        void Validate( RtspMessageHeaderCollection source );

        void Validate( RtspMessageHeaderCollection source , string header );

        void Reset();
    }
}
