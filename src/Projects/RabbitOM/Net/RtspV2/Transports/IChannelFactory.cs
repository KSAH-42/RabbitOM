using System;

namespace RabbitOM.Net.RtspV2.Transports
{
    public interface IChannelFactory
    {
        IChannel CreateChannel( EndPoint endpoint );

        IChannel CreateChannel( EndPoint endpoint , Binding binding );
    }
}
