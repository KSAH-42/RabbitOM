﻿using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent the base generic
    /// </summary>
    public partial class RtspQueue<TElement>
    {
        /// <summary>
        /// This class is used for updating the internal event handle during add and remove operations
        /// </summary>
        private sealed class Scope : IDisposable
        {
            private readonly RtspQueue<TElement> _queue;

            /// <summary>
            /// Initialize new instance of the scope class
            /// </summary>
            /// <param name="queue">the queue</param>
            /// <exception cref="ArgumentNullException"/>
            public Scope(RtspQueue<TElement> queue)
            {
                _queue = queue ?? throw new ArgumentNullException(nameof(queue));
            }

            /// <summary>
            /// Refresh the status
            /// </summary>
            public void Dispose()
            {
                if (_queue.Items.Count > 0)
                {
                    _queue.EventHandle.TrySet();
                }
                else
                {
                    _queue.EventHandle.TryReset();
                }
            }
        }
    }
}
