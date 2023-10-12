using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RabbitOM.Net.Rtps
{
    /// <summary>
    /// Represent a event circular queue
    /// </summary>
    public sealed class RTSPEventQueue : IEnumerable<EventArgs>
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

        private readonly Queue<EventArgs>    _queue             = new Queue<EventArgs>();

        private readonly int                 _maximumOfEvents   = DefaultMaximumOfEvents;




        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPEventQueue()
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
                    return _queue.Count;
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
                    return _queue.Count <= 0;
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
        /// Gets the enumerator
        /// </summary>
        /// <returns>returns an enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            lock ( _lock )
            {
                return _queue.ToList().GetEnumerator();
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
                return _queue.ToList().GetEnumerator();
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
                return _queue.Count > 0;
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
                while ( _queue.Count >= _maximumOfEvents )
                {
                    _queue.Dequeue();
                }

                _queue.Enqueue( eventArgs );

                UpdateStatusHandle();

                return true;
            }
        }

        /// <summary>
        /// Dequeue an event
        /// </summary>
        /// <returns>returns an instance, otherwise null</returns>
        public EventArgs Dequeue()
        {
            lock ( _lock )
            {
                EventArgs result = null;

                if ( _queue.Count > 0 )
                {
                    result = _queue.Dequeue();
                }

                UpdateStatusHandle();

                return result ?? EventArgs.Empty;
            }
        }

        /// <summary>
        /// Dequeue an event
        /// </summary>
        /// <param name="eventArgs">the event args</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool Dequeue( out EventArgs eventArgs )
        {
            eventArgs = null;

            lock ( _lock )
            {
                if ( _queue.Count > 0 )
                {
                    eventArgs = _queue.Dequeue();
                }

                UpdateStatusHandle();

                return eventArgs != null;
            }
        }

        /// <summary>
        /// Clear the queue
        /// </summary>
        public void Clear()
        {
            lock ( _lock )
            {
                _queue.Clear();

                UpdateStatusHandle();
            }
        }

        /// <summary>
        /// Wait until an element has been push to the queue
        /// </summary>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool Wait()
        {
            return _handle.Wait();
        }

        /// <summary>
        /// Wait until an element has been push to the queue
        /// </summary>
        /// <param name="cancellationHandle">the cancellation handle</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool Wait( EventWaitHandle cancellationHandle )
        {
            return _handle.Wait( cancellationHandle );
        }

        /// <summary>
        /// Wait until an element has been push to the queue
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool Wait( int timeout )
        {
            return _handle.Wait( timeout );
        }

        /// <summary>
        /// Wait until an element has been push to the queue
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <param name="cancellationHandle">the cancellation handle</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool Wait( int timeout , EventWaitHandle cancellationHandle )
        {
            return _handle.Wait( timeout , cancellationHandle );
        }

        /// <summary>
        /// Update the status handle
        /// </summary>
        private void UpdateStatusHandle()
        {
            if (_queue.Count > 0 )
            {
                _handle.Set();
            }
            else
            {
                _handle.Reset();
            }
        }
    }
}
