using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent the base generic
    /// </summary>
    internal partial class RTSPQueue<TElement>
    {
        /// <summary>
        /// This class is used for updating the internal event handle during add and remove operations
        /// </summary>
        sealed class Scope : IDisposable
        {
            private readonly RTSPQueue<TElement> _queue;

            /// <summary>
            /// Initialize new instance of the scope class
            /// </summary>
            /// <param name="queue">the queue</param>
            /// <exception cref="ArgumentNullException"/>
            public Scope(RTSPQueue<TElement> queue)
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
