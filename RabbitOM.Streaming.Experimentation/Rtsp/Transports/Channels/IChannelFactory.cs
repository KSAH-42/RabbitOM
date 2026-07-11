using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public interface IChannelFactory
    {
        IChannel CreateChannel( EndPoint endpoint );

        IChannel CreateChannel( EndPoint endpoint , Binding binding );
    }
}
