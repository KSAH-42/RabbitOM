using System;

namespace RabbitOM.Streaming.RtspV2.Transports.Channels
{
    public interface IChannelFactory
    {
        IChannel CreateChannel( EndPoint endpoint );

        IChannel CreateChannel( EndPoint endpoint , Binding binding );
    }
}
