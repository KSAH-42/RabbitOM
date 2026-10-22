using System;

namespace RabbitOM.Net.RtspV2.Transports
{
    public sealed class RtspClientChannelFactory : IChannelFactory
    {
        public IChannel CreateChannel( EndPoint endpoint )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public IChannel CreateChannel( EndPoint endpoint , Binding binding )
        {
            throw new NotImplementedException( "To be implemented" );
        }
    }
}
