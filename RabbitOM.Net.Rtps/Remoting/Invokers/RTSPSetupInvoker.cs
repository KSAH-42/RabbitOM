using System;

namespace RabbitOM.Net.Rtps.Remoting.Invokers
{
    /// <summary>
    /// Represent the proxy invoker
    /// </summary>
    public sealed class RTSPSetupInvoker : RTSPInvoker
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        internal RTSPSetupInvoker( RTSPProxy proxy )
            : base( proxy , RTSPMethodType.Setup )
        {
        }

        /// <summary>
        /// Sets the track uri
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public IRTSPInvoker SetTrackUri( string value )
        {
            Builder.ControlUri = value;

            return this;
        }

        /// <summary>
        /// Sets the delivery mode
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns an instance</returns>
        public IRTSPInvoker SetDeliveryMode( RTSPDeliveryMode value )
        {
            Builder.DeliveryMode = value;

            return this;
        }

        /// <summary>
        /// Sets the unicast port
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns an instance</returns>
        public IRTSPInvoker SetUnicastPort( int value )
        {
            Builder.UnicastPort = value;

            return this;
        }

        /// <summary>
        /// Sets the multicast address
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns an instance</returns>
        public IRTSPInvoker SetMulticastAddress( string value )
        {
            Builder.MulticastAddress = value;

            return this;
        }

        /// <summary>
        /// Sets the multicast port
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns an instance</returns>
        public IRTSPInvoker SetMulticastPort( int value )
        {
            Builder.MulticastPort = value;

            return this;
        }

        /// <summary>
        /// Sets the multicast port
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns an instance</returns>
        public IRTSPInvoker SetMulticastTTL( byte value )
        {
            Builder.TTL = value;

            return this;
        }
    }
}
