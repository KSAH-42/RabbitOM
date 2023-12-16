using System;

namespace RabbitOM.Net.Rtsp
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
        /// <param name="data">the data</param>
        /// <exception cref="ArgumentNullException"/>
        public RTSPPacketReceivedEventArgs( byte[] data )
            : this( new RTSPPacket( data ) )
        {
        }

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
