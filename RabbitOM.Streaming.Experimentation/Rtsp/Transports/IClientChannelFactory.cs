using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    public interface IClientChannelFactory 
    {
        IClientChannel CreateChannel( EndPoint endpoint );
    }
}
