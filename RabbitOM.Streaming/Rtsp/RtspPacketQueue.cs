using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a packet circular queue
    /// </summary>
    public sealed class RtspPacketQueue : RtspQueue<RtspPacket>
    {
        private readonly int _maximum  = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        public RtspPacketQueue()
            : this( 32000 )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="maximum ">the maximum of packets allowed</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public RtspPacketQueue( int maximum )
        {
            _maximum = maximum > 0 ? maximum : throw new ArgumentOutOfRangeException(nameof(maximum));
        }

        /// <summary>
        /// Occurs during a custom validaton
        /// </summary>
        /// <param name="packet">the packet</param>
        /// <returns>returns true for a success, otherwise false</returns>
        protected override bool OnValidate( RtspPacket packet )
        {
            return packet != null;
        }

        /// <summary>
        /// Occurs before adding an element
        /// </summary>
        /// <param name="packet">the packet</param>
        protected override void OnEnqueue( RtspPacket packet )
        {
            while ( Items.Count >= _maximum)
            {
                Items.Dequeue();
            }
        }
    }
}
