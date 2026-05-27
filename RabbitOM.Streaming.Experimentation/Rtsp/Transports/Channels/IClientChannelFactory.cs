using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public interface IClientChannelFactory
    {
        IClientChannel CreateChannel( EndPoint endpoint );

        IClientChannel CreateChannel( EndPoint endpoint , Binding binding );
    }
}
