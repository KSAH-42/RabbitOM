using System;

namespace RabbitOM.Streaming.Net.Rtsp
{
    /// <summary>
    /// Represent an event args
    /// </summary>
    public class RtspPacketReceivedEventArgs : EventArgs
    {
        private readonly RtspPacket _packet = null;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">the data</param>
        /// <exception cref="ArgumentNullException"/>
        public RtspPacketReceivedEventArgs( byte[] data )
            : this( new RtspPacket( data ) )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="packet">the packet</param>
        /// <exception cref="ArgumentNullException"/>
        public RtspPacketReceivedEventArgs( RtspPacket packet )
        {
            _packet = packet ?? throw new ArgumentNullException( nameof( packet ) );
        }

        /// <summary>
        /// Gets the packet
        /// </summary>
        public RtspPacket Packet
        {
            get => _packet;
        }
    }
}
