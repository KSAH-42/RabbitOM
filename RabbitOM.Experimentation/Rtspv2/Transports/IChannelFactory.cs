using System;

namespace RabbitOM.Streaming.RtspV2.Transports
{
    public interface IChannelFactory
    {
        IChannel CreateChannel( EndPoint endpoint );

        IChannel CreateChannel( EndPoint endpoint , Binding binding );
    }
}
