using System;

namespace RabbitOM.Net.Rtsp.Remoting
{
    /// <summary>
    /// Represent an event args
    /// </summary>
    public class RTSPConnectionDataReceivedEventArgs : EventArgs
    {
        private readonly RTSPPacket _packet = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="packet">the packet</param>
        /// <exception cref="ArgumentNullException"/>
        public RTSPConnectionDataReceivedEventArgs( RTSPPacket packet )
        {
            _packet = packet ?? throw new ArgumentNullException( nameof( packet ) );
        }

        /// <summary>
        /// Gets the packet
        /// </summary>
        public RTSPPacket Packet
        {
            get => _packet;
        }
    }
}
