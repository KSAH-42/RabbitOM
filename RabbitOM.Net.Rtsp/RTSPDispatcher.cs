using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a dispatcher queue
    /// </summary>
    public sealed class RTSPDispatcher : IEnumerable<Action>
    {
        /// <summary>
        /// Represent the maximum of element that can be queued
        /// </summary>
        public const int                 MaximumSize  = 300000;



        private readonly object          _lock        = new object();

        private readonly Queue<Action>   _queue       = new Queue<Action>();

        private readonly EventWaitHandle _handle      = new EventWaitHandle( false , EventResetMode.ManualReset );

        private int                      _capacity    = MaximumSize;



        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPDispatcher()
        {
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
        /// Gets / Sets the capacity
        /// </summary>
        public int Capacity
        {
            get
            {
                lock ( _lock )
                {
                    return _capacity;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _capacity = value > 0
                             ? value < MaximumSize ? value : MaximumSize
                             : 0;

                    AdjustQueue( _capacity );
                    UpdateStatusHandle();
                }
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
                return _queue.ToList().GetEnumerator();
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
        /// Post an action
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
                bool success = false;

                AdjustQueue( _capacity + 1 );

                if ( _capacity > 0 )
                {
                    _queue.Enqueue( action );

                    success = true;
                }

                UpdateStatusHandle();

                return success;
            }
        }

        /// <summary>
        /// Dequeue an action
        /// </summary>
        /// <param name="action">the action</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool Dequeue( out Action action )
        {
            action = null;

            lock ( _lock )
            {
                AdjustQueue( _capacity );

                if ( _queue.Count > 0 )
                {
                    action = _queue.Dequeue();
                }

                UpdateStatusHandle();

                return action != null;
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
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool Wait( TimeSpan timeout )
        {
            try
            {
                return _handle.WaitOne( timeout );
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex );
            }

            return false;
        }

        /// <summary>
        /// Wait until an element has been push to the queue
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <param name="cancellationHandle">the cancellation handle</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool Wait( TimeSpan timeout , EventWaitHandle cancellationHandle )
        {
            if ( cancellationHandle == null )
            {
                return false;
            }

            try
            {
                var handles = new EventWaitHandle[]
                {
                    cancellationHandle , _handle
                };

                return EventWaitHandle.WaitAny( handles , timeout ) == 1;
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex );
            }

            return false;
        }

        /// <summary>
        /// Adjus the queue to the actual size
        /// </summary>
        /// <param name="limit">the limit</param>
        private void AdjustQueue( int limit )
        {
            limit = limit > 0 ? limit : 0;

            while ( _queue.Count > limit )
            {
                _queue.Dequeue();
            }
        }

        /// <summary>
        /// Update the status handle
        /// </summary>
        private void UpdateStatusHandle()
        {
            try
            {
                if ( _queue.Count > 0 )
                {
                    _handle.Set();
                }
                else
                {
                    _handle.Reset();
                }
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex );
            }
        }
    }
}
