using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public interface IBuilderConfigurer<TConfiguration> where TConfiguration : class
    {
        void Configure( TConfiguration configuration );
    }
}
