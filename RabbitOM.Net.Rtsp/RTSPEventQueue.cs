using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a event circular queue
    /// </summary>
    internal sealed class RTSPEventQueue : RTSPQueue<EventArgs>
    {
        private readonly int _maximum = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPEventQueue()
            : this( 16000 )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="maximum ">the maximum of packets allowed</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public RTSPEventQueue( int maximum )
        {
            _maximum = maximum > 0 ? maximum : throw new ArgumentOutOfRangeException( nameof( maximum ) );
        }

        /// <summary>
        /// Occurs during a custom validaton
        /// </summary>
        /// <param name="eventArgs">the action</param>
        /// <returns>returns true for a success, otherwise false</returns>
        protected override bool OnValidate( EventArgs eventArgs )
        {
            return eventArgs != null;
        }

        /// <summary>
        /// Occurs before adding an element
        /// </summary>
        /// <param name="eventArgs">the event args</param>
        protected override void OnEnqueue( EventArgs eventArgs )
        {
            while ( Items.Count >= _maximum )
            {
                Items.Dequeue();
            }
        }
    }
}
