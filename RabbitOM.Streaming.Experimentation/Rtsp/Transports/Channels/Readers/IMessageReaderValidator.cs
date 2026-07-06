using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    // TODO: try to improve the design of the validator, the headers collection are actuallay passed as pararameter and never capture by the validator just in case if an exception occurs during the read
    public interface IMessageReaderValidator
    {
        void Validate( RtspMessageHeaderCollection source );

        void Validate( RtspMessageHeaderCollection source , string header );

        void Setup();
    }
}
