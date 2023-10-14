using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a event circular queue
    /// </summary>
    public sealed partial class RTSPEventQueue 
    {
        /// <summary>
        /// This class is used to update the internal event handle
        /// </summary>
        class Scope : IDisposable
        {
            private readonly RTSPEventQueue _queue;

            /// <summary>
            /// Initiaize new instance of scope
            /// </summary>
            /// <param name="queue">the queue</param>
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
