using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a chunk circular queue
    /// </summary>
    public sealed class RtspChunkQueue : RtspQueue<byte[]>
    {
        private readonly int _maximum = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        public RtspChunkQueue()
            : this( 32000 )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="maximum ">the maximum of packets allowed</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public RtspChunkQueue( int maximum )
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
