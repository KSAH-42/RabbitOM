using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a event circular queue
    /// </summary>
    internal sealed partial class RTSPEventQueue 
    {
        /// <summary>
        /// This class is used to update the internal event handle
        /// </summary>
        sealed class Scope : IDisposable
        {
            private readonly RTSPEventQueue _queue;

            /// <summary>
            /// Initialize new instance of the scope class
            /// </summary>
            /// <param name="queue">the queue</param>
            /// <exception cref="ArgumentNullException"/>
	    public Scope(RTSPEventQueue queue)
            {
                _queue = queue ?? throw new ArgumentNullException(nameof(queue));
            }

            /// <summary>
            /// Refresh the status
            /// </summary>
            public void Dispose()
            {
                if (_queue.Count > 0)
                {
                    _queue.Handle.Set();
                }
                else
                {
                    _queue.Handle.Reset();
                }
            }
        }
    }
}
