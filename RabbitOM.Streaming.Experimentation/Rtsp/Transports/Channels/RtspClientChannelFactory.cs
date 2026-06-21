using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public sealed class RtspClientChannelFactory : IClientChannelFactory
    {
        public IClientChannel CreateChannel( EndPoint endpoint )
        {
            throw new NotImplementedException();
        }

        public IClientChannel CreateChannel( EndPoint endpoint , Binding binding )
        {
            throw new NotImplementedException();
        }
    }
}
