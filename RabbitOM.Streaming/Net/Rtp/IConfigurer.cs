using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public interface IConfigurer<TConfiguration> where TConfiguration : class
    {
        void Configure( TConfiguration configuration );
    }
}
