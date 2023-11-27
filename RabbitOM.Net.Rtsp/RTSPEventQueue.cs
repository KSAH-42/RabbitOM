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
    internal sealed class RTSPEventQueue : RTSPBaseQueue<EventArgs>
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
        public override object SyncRoot
        {
            get => _lock;
        }

        /// <summary>
        /// Gets the number of elements
        /// </summary>
        public override int Count
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
        public override bool IsEmpty
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
        protected override RTSPEventWaitHandle Handle
        {
            get => _handle;
        }









        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>returns an enumerator</returns>
        protected override IEnumerator<EventArgs> BaseGetEnumerator()
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
        public override bool Any()
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
        public override bool Enqueue( EventArgs eventArgs )
        {
            if ( eventArgs == null )
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
        /// <returns>returns an instance, otherwise null</returns>
        public override EventArgs Dequeue()
        {
            lock ( _lock )
            {
                using ( _scope )
                {
                    return _collection.Count > 0 ? _collection.Dequeue() : EventArgs.Empty;
                }
            }
        }

        /// <summary>
        /// Dequeue an event
        /// </summary>
        /// <param name="result">the event args</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public override bool TryDequeue( out EventArgs result )
        {
            lock ( _lock )
            {
                using ( _scope )
                {
                    result = _collection.Count > 0 ? _collection.Dequeue() : null;

                    return result != null;
                }
            }
        }

        /// <summary>
        /// Clear the queue
        /// </summary>
        public override void Clear()
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
