using System;

namespace RabbitOM.Streaming.RtspV2.Transports.Channels
{
    public sealed class RtspClientChannelFactory : IChannelFactory
    {
        public IChannel CreateChannel( EndPoint endpoint )
        {
            throw new NotImplementedException();
        }

        public IChannel CreateChannel( EndPoint endpoint , Binding binding )
        {
            throw new NotImplementedException();
        }
    }
}
