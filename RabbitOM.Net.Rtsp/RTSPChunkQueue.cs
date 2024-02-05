using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a chunk circular queue
    /// </summary>
    internal sealed class RTSPChunkQueue : RTSPQueue<byte[]>
    {
        private readonly int _maximum = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPChunkQueue()
            : this( 32000 )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="maximum ">the maximum of packets allowed</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public RTSPChunkQueue( int maximum )
        {
            _maximum = maximum > 0 ? maximum : throw new ArgumentOutOfRangeException(nameof(maximum));
        }

        /// <summary>
        /// Occurs during a custom validaton
        /// </summary>
        /// <param name="chunk">the chunk</param>
        /// <returns>returns true for a success, otherwise false</returns>
        protected override bool OnValidate( byte[] chunk)
        {
            return chunk != null && chunk.Length > 0;
        }

        /// <summary>
        /// Occurs before adding an element
        /// </summary>
        /// <param name="chunk">the chunk</param>
        protected override void OnEnqueue( byte[] chunk )
        {
            while ( Items.Count >= _maximum )
            {
                Items.Dequeue();
            }
        }
    }
}
