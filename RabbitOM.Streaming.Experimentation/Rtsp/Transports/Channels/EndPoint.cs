using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public sealed class EndPoint
    {
        public EndPoint( string address , ushort port )
        {
            if ( string.IsNullOrWhiteSpace( address ) )
            {
                throw new ArgumentException( nameof( address ) );
            }

            Address = address;

            Port = port;
        }

        public string Address { get; }

        public ushort Port { get; }
    }
}
