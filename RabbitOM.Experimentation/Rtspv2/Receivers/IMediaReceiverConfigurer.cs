using System;

namespace RabbitOM.Streaming.RtspV2.Receivers
{
    public interface IMediaReceiverConfigurer<TConfiguration> where TConfiguration : RtspMediaReceiverConfiguration
    {
        void Configure( TConfiguration configuration );
    }
}
