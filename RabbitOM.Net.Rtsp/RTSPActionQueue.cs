using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent an action queue
    /// </summary>
    internal sealed partial class RTSPActionQueue : IEnumerable<Action>
    {
        private readonly object              _lock              = null;

        private readonly RTSPEventWaitHandle _handle            = null;

        private readonly Queue<Action>       _collection        = null;

        private readonly Scope               _scope             = null;







        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPActionQueue()
        {
            _lock            = new object();
            _collection      = new Queue<Action>();
            _handle          = new RTSPEventWaitHandle();
            _scope           = new Scope( this );
        }







        /// <summary>
        /// Gets the sync root
        /// </summary>
        public object SyncRoot
        {
            get => _lock;
        }

        /// <summary>
        /// Gets the number of elements
        /// </summary>
        public int Count
        {
            get
            {
                lock ( _lock )
                {
                    return _collection.Count;
                }
            }
        }

        /// <summary>
        /// Check if the queue is empty
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                lock ( _lock )
                {
                    return _collection.Count <= 0;
                }
            }
        }

        /// <summary>
        /// Gets the handle
        /// </summary>
        private RTSPEventWaitHandle Handle
        {
            get => _handle;
        }







        /// <summary>
        /// Wait until an element has been push to the queue
        /// </summary>
        /// <param name="queue">the queue</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        /// <exception cref="ArgumentNullException"/>
        public static bool Wait(RTSPActionQueue queue)
        {
            if (queue == null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            return queue.Handle.Wait();
        }

        /// <summary>
        /// Wait until an element has been push to the queue
        /// </summary>
        /// <param name="queue">the queue</param>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        /// <exception cref="ArgumentNullException"/>
        public static bool Wait(RTSPActionQueue queue, int timeout)
        {
            if (queue == null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            return queue.Handle.Wait(timeout);
        }

        /// <summary>
        /// Wait until an element has been push to the queue
        /// </summary>
        /// <param name="queue">the queue</param>
        /// <param name="cancellationHandle">the cancellation handle</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        /// <exception cref="ArgumentNullException"/>
        public static bool Wait(RTSPActionQueue queue, EventWaitHandle cancellationHandle)
        {
            if (queue == null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            return queue.Handle.Wait(cancellationHandle);
        }

        /// <summary>
        /// Wait until an element has been push to the queue
        /// </summary>
        /// <param name="queue">the queue</param>
        /// <param name="timeout">the timeout</param>
        /// <param name="cancellationHandle">the cancellation handle</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        /// <exception cref="ArgumentNullException"/>
        public static bool Wait(RTSPActionQueue queue, int timeout, EventWaitHandle cancellationHandle)
        {
            if (queue == null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            if (cancellationHandle == null)
            {
                throw new ArgumentNullException(nameof(cancellationHandle));
            }

            return queue.Handle.Wait(timeout, cancellationHandle);
        }








        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>returns an enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            lock ( _lock )
            {
                return _collection.ToList().GetEnumerator();
            }
        }

        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>returns an enumerator</returns>
        public IEnumerator<Action> GetEnumerator()
        {
            lock ( _lock )
            {
                return _collection.ToList().GetEnumerator();
            }
        }

        /// <summary>
        /// Check if the queue contains some elements
        /// </summary>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool Any()
        {
            lock ( _lock )
            {
                return _collection.Count > 0;
            }
        }

        /// <summary>
        /// Post an element
        /// </summary>
        /// <param name="action">the action</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool Enqueue( Action action )
        {
            if ( action == null )
			{
                return false;
			}
        
            lock ( _lock )
            {
                using (_scope )
                {
                    _collection.Enqueue( action );

                    return true;
                }
            }
        }

        /// <summary>
        /// Dequeue an action
        /// </summary>
        /// <returns>must returns an instance</returns>
        public Action Dequeue()
        {
            lock ( _lock )
            {
                using ( _scope )
                {
                    return _collection.Dequeue();
                }
            }
        }

        /// <summary>
        /// Dequeue an action
        /// </summary>
        /// <param name="action">the action</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool TryDequeue( out Action action )
        {
            lock ( _lock )
            {
                using ( _scope )
                {
                    action = _collection.Count > 0 ? _collection.Dequeue() : null;

                    return action != null;
                }
            }
        }

        /// <summary>
        /// Clear the queue
        /// </summary>
        public void Clear()
        {
            lock ( _lock )
            {
                using ( _scope )
                {
                    _collection.Clear();
                }
            }
        }
    }
}
