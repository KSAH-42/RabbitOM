using System;

namespace RabbitOM.Streaming.Net.RtspV2.Receivers
{
    public interface IReceiverConfigurer<TConfiguration> where TConfiguration : RtspReceiverConfiguration
    {
        void Configure( TConfiguration configuration );
    }
}
