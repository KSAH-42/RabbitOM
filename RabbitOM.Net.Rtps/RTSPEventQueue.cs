using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a event circular queue
    /// </summary>
    public sealed partial class RTSPEventQueue : IEnumerable<EventArgs>
    {
        /// <summary>
        /// Represent the maximum of events
        /// </summary>
        public const int                      DefaultMaximumOfEvents = 16000;
        
        /// <summary>
        /// Represent the default offset trigger
        /// </summary>
        public const int                      DefaultOffsetTrigger   = 0;



        private readonly object              _lock              = new object();

        private readonly RTSPEventWaitHandle _handle            = new RTSPEventWaitHandle();

        private readonly Queue<EventArgs>    _collection        = new Queue<EventArgs>();

        private readonly int                 _maximumOfEvents   = DefaultMaximumOfEvents;

        private readonly Scope               _scope             = null;




        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPEventQueue()
            : this( DefaultMaximumOfEvents , DefaultOffsetTrigger )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="maximumOfEvents">the maximum of events allowed</param>
        /// <param name="triggerOffset">the trigger offset</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public RTSPEventQueue( int maximumOfEvents , int triggerOffset )
        {
            _maximumOfEvents = maximumOfEvents >  0 ? maximumOfEvents : throw new ArgumentOutOfRangeException( nameof( maximumOfEvents ) );

            _lock   = new object();
            _handle = new RTSPEventWaitHandle();
            _collection  = new Queue<EventArgs>();
            _scope  = new Scope(this);
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
        /// Gets the maximum of pending events
        /// </summary>
        public int MaximumOfEvents
        {
            get
            {
                lock (_lock)
                {
                    return _maximumOfEvents;
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
        public static bool Wait(RTSPEventQueue queue)
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
        public static bool Wait(RTSPEventQueue queue, int timeout)
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
        public static bool Wait(RTSPEventQueue queue, EventWaitHandle cancellationHandle)
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
        public static bool Wait(RTSPEventQueue queue, int timeout, EventWaitHandle cancellationHandle)
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
        public IEnumerator<EventArgs> GetEnumerator()
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
        /// Enqueue an event
        /// </summary>
        /// <param name="eventArgs">the event args</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool Enqueue( EventArgs eventArgs )
        {
            if ( eventArgs == null || object.ReferenceEquals( eventArgs , EventArgs.Empty ) )
            {
                return false;
            }

            lock ( _lock )
            {
                using ( _scope )
                {
                    while ( _collection.Count >= _maximumOfEvents )
                    {
                        _collection.Dequeue();
                    }

                    _collection.Enqueue( eventArgs );

                    return true;
                }
            }
        }

        /// <summary>
        /// Dequeue an event
        /// </summary>
        /// <returns>must returns an instance</returns>
        public EventArgs Dequeue()
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
        /// Dequeue an event
        /// </summary>
        /// <param name="eventArgs">the event args</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool TryDequeue( out EventArgs eventArgs )
        {
            lock ( _lock )
            {
                using ( _scope )
                {
                    eventArgs = _collection.Count > 0 ? _collection.Dequeue() : null;

                    return eventArgs != null;
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
