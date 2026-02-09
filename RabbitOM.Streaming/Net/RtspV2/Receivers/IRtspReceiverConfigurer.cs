using System;

namespace RabbitOM.Streaming.Net.RtspV2.Receivers
{
    public interface IRtspReceiverConfigurer<TConfiguration> where TConfiguration : RtspReceiverConfiguration
    {
        void Configure( TConfiguration configuration );
    }
}
