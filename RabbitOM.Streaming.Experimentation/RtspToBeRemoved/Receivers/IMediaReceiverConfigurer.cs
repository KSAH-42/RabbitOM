using System;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Receivers
{
    public interface IMediaReceiverConfigurer<TConfiguration> where TConfiguration : RtspMediaReceiverConfiguration
    {
        void Configure( TConfiguration configuration );
    }
}
