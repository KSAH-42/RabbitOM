using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent the base generic
    /// </summary>
    internal partial class RTSPBaseQueue<TElement>
    {
        /// <summary>
        /// This class is used to update the internal event handle
        /// </summary>
        public sealed class Scope : IDisposable
        {
            private readonly RTSPBaseQueue<TElement> _queue;

            /// <summary>
            /// Initialize new instance of the scope class
            /// </summary>
            /// <param name="queue">the queue</param>
            /// <exception cref="ArgumentNullException"/>
	        public Scope(RTSPBaseQueue<TElement> queue)
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
