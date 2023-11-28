using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a packet circular queue
    /// </summary>
    internal sealed class RTSPPacketQueue : RTSPQueue<RTSPPacket>
    {
        private readonly int _maximum  = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPPacketQueue()
            : this( 32000 )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="maximum ">the maximum of packets allowed</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public RTSPPacketQueue( int maximum )
        {
            _maximum = maximum > 0 ? maximum : throw new ArgumentOutOfRangeException(nameof(maximum));
        }

        /// <summary>
        /// Occurs during a custom validaton
        /// </summary>
        /// <param name="packet">the packet</param>
        /// <returns>returns true for a success, otherwise false</returns>
        protected override bool OnValidate( RTSPPacket packet )
        {
            return packet != null;
        }

        /// <summary>
        /// Occurs before adding an element
        /// </summary>
        /// <param name="packet">the packet</param>
        protected override void OnEnqueue( RTSPPacket packet )
        {
            while ( Items.Count >= _maximum)
            {
                Items.Dequeue();
            }
        }
    }
}
