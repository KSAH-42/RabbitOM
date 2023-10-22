using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a packet circular queue
    /// </summary>
    public sealed partial class RTSPPacketQueue
    {
        /// <summary>
        /// This class is used to update the internal event handle
        /// </summary>
        sealed class Scope : IDisposable
        {
            private readonly RTSPPacketQueue _queue;

            /// <summary>
            /// Initialize new instance of the scope class
            /// </summary>
            /// <param name="queue">the queue</param>
            /// <exception cref="ArgumentNullException"/>
	    public Scope(RTSPPacketQueue queue)
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
