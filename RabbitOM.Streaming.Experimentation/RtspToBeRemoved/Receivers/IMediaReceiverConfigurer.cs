using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Receivers
{
    public interface IMediaReceiverConfigurer<TConfiguration> where TConfiguration : RtspMediaReceiverConfiguration
    {
        void Configure( TConfiguration configuration );
    }
}
