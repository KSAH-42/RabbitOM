using System;

namespace RabbitOM.Streaming.Rtsp.Clients.Connections.Invokers
{
    /// <summary>
    /// Represent the proxy invoker
    /// </summary>
    public sealed class RtspSetupInvoker : RtspInvoker
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        internal RtspSetupInvoker( RtspProxy proxy )
            : base( proxy , RtspMethod.Setup )
        {
        }

        /// <summary>
        /// Sets the track uri
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public IRtspInvoker SetTrackUri( string value )
        {
            Builder.ControlUri = value;

            return this;
        }

        /// <summary>
        /// Sets the delivery mode
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns an instance</returns>
        public IRtspInvoker SetDeliveryMode( RtspDeliveryMode value )
        {
            Builder.DeliveryMode = value;

            return this;
        }

        /// <summary>
        /// Sets the unicast port
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns an instance</returns>
        public IRtspInvoker SetUnicastPort( int value )
        {
            Builder.UnicastPort = value;

            return this;
        }

        /// <summary>
        /// Sets the multicast address
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns an instance</returns>
        public IRtspInvoker SetMulticastAddress( string value )
        {
            Builder.MulticastAddress = value;

            return this;
        }

        /// <summary>
        /// Sets the multicast port
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns an instance</returns>
        public IRtspInvoker SetMulticastPort( int value )
        {
            Builder.MulticastPort = value;

            return this;
        }

        /// <summary>
        /// Sets the multicast port
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns an instance</returns>
        public IRtspInvoker SetMulticastTTL( byte value )
        {
            Builder.TTL = value;

            return this;
        }
    }
}
