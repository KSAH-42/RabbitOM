using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers
{
    public interface IReceiverConfigurer<TConfiguration> where TConfiguration : RtspMediaReceiverConfiguration
    {
        void Configure( TConfiguration configuration );
    }
}
