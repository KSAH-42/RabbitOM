using System;

namespace RabbitOM.Net.Rtps
{
    /// <summary>
    /// Represent an event args
    /// </summary>
    public class RTSPPacketReceivedEventArgs : EventArgs
    {
        private readonly RTSPPacket _packet = null;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="packet">the packet</param>
        /// <exception cref="ArgumentNullException"/>
        public RTSPPacketReceivedEventArgs( RTSPPacket packet )
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
