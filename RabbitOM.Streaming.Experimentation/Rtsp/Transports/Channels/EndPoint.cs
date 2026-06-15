using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public sealed class EndPoint
    {
        public EndPoint( string address , int port )
        {
            if ( string.IsNullOrWhiteSpace( address ) )
            {
                throw new ArgumentException( nameof( address ) );
            }

            if ( port < 0 )
            {
                throw new ArgumentException( nameof( port ) );
            }

            Address = address;

            Port = port;
        }

        public string Address { get; }

        public int Port { get; }
    }
}
