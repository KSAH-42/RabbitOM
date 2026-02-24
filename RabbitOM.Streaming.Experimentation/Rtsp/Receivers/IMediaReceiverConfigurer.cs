using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers
{
    public interface IMediaReceiverConfigurer<TConfiguration> where TConfiguration : RtspMediaReceiverConfiguration
    {
        void Configure( TConfiguration configuration );
    }
}
