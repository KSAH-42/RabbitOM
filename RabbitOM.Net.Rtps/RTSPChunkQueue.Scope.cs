using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a chunk circular queue
    /// </summary>
    public sealed partial class RTSPChunkQueue 
    {
        /// <summary>
        /// This class is used to update the internal event handle
        /// </summary>
        class Scope : IDisposable
        {
            private readonly RTSPChunkQueue _queue;

            /// <summary>
            /// Initiaize new instance of scope
            /// </summary>
            /// <param name="queue">the queue</param>
			public Scope(RTSPChunkQueue queue)
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
