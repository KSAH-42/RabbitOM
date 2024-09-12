using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent the base generic queue
    /// </summary>
    /// <typeparam name="TElement">the element type</typeparam>
    public partial class RtspQueue<TElement> 
        : IEnumerable 
        , IEnumerable<TElement>
        , ICollection
        , IReadOnlyCollection<TElement>
    {
        private readonly object _lock;

        private readonly EventWaitHandle _handle;

        private readonly Queue<TElement> _collection;

        private readonly Scope _scope;






        /// <summary>
        /// Initilize a new instance of queue
        /// </summary>
        public RtspQueue()
            : this ( 1000 )
        {
        }

        /// <summary>
        /// Initilize a new instance of queue
        /// </summary>
        /// <param name="capacity">the capacity</param>
        public RtspQueue( int capacity )
        {
            if ( capacity <= 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( capacity ) );
            }

            _lock       = new object();
            _collection = new Queue<TElement>( capacity );
            _handle     = new ManualResetEvent( false );
            _scope      = new Scope( this );
        }






        /// <summary>
        /// Gets the sync root
        /// </summary>
        public object SyncRoot
        {
            get
            {
                return _lock;
            }
        }

        /// <summary>
        /// Returns true
        /// </summary>
        public bool IsSynchronized
        {
            get
            {
                return true;
            }
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
                    return _collection.Count <= 0 ;
                }
            }
        }

        /// <summary>
        /// Gets the inner collection
        /// </summary>
        protected Queue<TElement> Items
        {
            get
            {
                return _collection;
            }
        }

        /// <summary>
        /// Gets the handle
        /// </summary>
        private EventWaitHandle Handle
        {
            get
            {
                return _handle;
            }
        }






        /// <summary>
        /// Wait until an element has been push to the queue
        /// </summary>
        /// <param name="queue">the queue</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        /// <exception cref="ArgumentNullException"/>
        public static bool Wait( RtspQueue<TElement> queue )
        {
            if ( queue == null )
            {
                throw new ArgumentNullException( nameof( queue ) );
            }

            return queue.Handle.TryWait();
        }

        /// <summary>
        /// Wait until an element has been push to the queue
        /// </summary>
        /// <param name="queue">the queue</param>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        /// <exception cref="ArgumentNullException"/>
        public static bool Wait( RtspQueue<TElement> queue , int timeout )
        {
            if ( queue == null )
            {
                throw new ArgumentNullException( nameof( queue ) );
            }

            return queue.Handle.TryWait( timeout );
        }

        /// <summary>
        /// Wait until an element has been push to the queue
        /// </summary>
        /// <param name="queue">the queue</param>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        /// <exception cref="ArgumentNullException"/>
        public static bool Wait( RtspQueue<TElement> queue , TimeSpan timeout )
        {
            if (queue == null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            return queue.Handle.TryWait( timeout);
        }

        /// <summary>
        /// Wait until an element has been push to the queue
        /// </summary>
        /// <param name="queue">the queue</param>
        /// <param name="cancellationHandle">the cancellation handle</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        /// <exception cref="ArgumentNullException"/>
        public static bool Wait( RtspQueue<TElement> queue , EventWaitHandle cancellationHandle )
        {
            if ( queue == null )
            {
                throw new ArgumentNullException( nameof( queue ) );
            }

            return queue.Handle.TryWait( cancellationHandle );
        }

        /// <summary>
        /// Wait until an element has been push to the queue
        /// </summary>
        /// <param name="queue">the queue</param>
        /// <param name="timeout">the timeout</param>
        /// <param name="cancellationHandle">the cancellation handle</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        /// <exception cref="ArgumentNullException"/>
        public static bool Wait(RtspQueue<TElement> queue, int timeout, EventWaitHandle cancellationHandle)
        {
            if ( queue == null )
            {
                throw new ArgumentNullException( nameof( queue ) );
            }

            if ( cancellationHandle == null )
            {
                throw new ArgumentNullException( nameof( cancellationHandle ) );
            }

            return queue.Handle.TryWait( timeout , cancellationHandle );
        }

        /// <summary>
        /// Wait until an element has been push to the queue
        /// </summary>
        /// <param name="queue">the queue</param>
        /// <param name="timeout">the timeout</param>
        /// <param name="cancellationHandle">the cancellation handle</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        /// <exception cref="ArgumentNullException"/>
        public static bool Wait(RtspQueue<TElement> queue, TimeSpan timeout, EventWaitHandle cancellationHandle)
        {
            if (queue == null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            if (cancellationHandle == null)
            {
                throw new ArgumentNullException(nameof(cancellationHandle));
            }

            return queue.Handle.TryWait( timeout, cancellationHandle);
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
        public IEnumerator<TElement> GetEnumerator()
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
        /// Copy the element to the targe array
        /// </summary>
        /// <param name="array">the target array</param>
        /// <param name="index">the index</param>
        public void CopyTo(Array array, int index)
        {
            lock ( _lock )
            {
                _collection.CopyTo( array as TElement[] , index );
            }
        }

        /// <summary>
        /// Enqueue an element
        /// </summary>
        /// <param name="element">the element</param>
        public void Enqueue( TElement element )
        {
            lock ( _lock )
            {
                using ( _scope )
                {
                    if ( OnValidate( element ) )
                    {
                        OnEnqueue( element );

                        _collection.Enqueue( element );
                    }
                }
            }
        }

        /// <summary>
        /// Dequeue an element
        /// </summary>
        /// <returns>must returns an instance</returns>
        public TElement Dequeue()
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
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool TryDequeue( out TElement result )
        {
            result = default;

            lock ( _lock )
            {
                using ( _scope )
                {
                    if ( _collection.Count <= 0 )
                    {
                        return false;
                    }

                    result = _collection.Dequeue();

                    return true;
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






        /// <summary>
        /// Occurs during a custom validaton
        /// </summary>
        /// <param name="element">the element</param>
        /// <returns>returns true for a success, otherwise false</returns>
        protected virtual bool OnValidate( TElement element )
        {
            return true;
        }

        /// <summary>
        /// Occurs before adding an element
        /// </summary>
        /// <param name="element">the element</param>
        protected virtual void OnEnqueue( TElement element )
        {
        }
    }
}
