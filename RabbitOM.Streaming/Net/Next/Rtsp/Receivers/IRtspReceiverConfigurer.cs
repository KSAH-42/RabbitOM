using System;

namespace RabbitOM.Streaming.Net.Next.Rtsp.Receivers
{
    public interface IRtspReceiverConfigurer<TConfiguration> where TConfiguration : RtspReceiverConfiguration
    {
        void Configure( TConfiguration configuration );
    }
}
