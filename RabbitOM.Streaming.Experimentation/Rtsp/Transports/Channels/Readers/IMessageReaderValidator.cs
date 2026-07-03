using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    // headers collection must not be own internal the validator in case of exception
    public interface IMessageReaderValidator
    {
        void Validate( RtspMessageHeaderCollection source );

        void Validate( RtspMessageHeaderCollection source , string header );

        void Reset();
    }
}
