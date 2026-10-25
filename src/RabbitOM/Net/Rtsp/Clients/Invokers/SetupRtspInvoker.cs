using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    public sealed class SetupRtspInvoker : RtspInvoker
    {
        internal SetupRtspInvoker( RtspProxy proxy ) : base( proxy , RtspMethod.Setup )
        {
        }

        public IRtspInvoker SetTrackUri( string value )
        {
            Builder.ControlUri = value;

            return this;
        }

        public IRtspInvoker SetDeliveryMode( RtspDeliveryMode value )
        {
            Builder.DeliveryMode = value;

            return this;
        }

        public IRtspInvoker SetUnicastPort( int value )
        {
            Builder.UnicastPort = value;

            return this;
        }

        public IRtspInvoker SetMulticastAddress( string value )
        {
            Builder.MulticastAddress = value;

            return this;
        }

        public IRtspInvoker SetMulticastPort( int value )
        {
            Builder.MulticastPort = value;

            return this;
        }

        public IRtspInvoker SetMulticastTTL( byte value )
        {
            Builder.TTL = value;

            return this;
        }
    }
}
