using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RabbitOM.Net.Rtps
{
    /// <summary>
    /// Represent a chunk circular queue
    /// </summary>
    public sealed class RTSPChunkQueue : IEnumerable<byte[]>
    {
        /// <summary>
        /// The default maximum size
        /// </summary>
        public const int                     DefaultMaximumSize = 32000;




        private readonly object              _lock              = null;

        private readonly RTSPEventWaitHandle _handle            = null;

        private readonly Queue<byte[]>       _queue             = null;

        private readonly int                 _maximumOfChunks   = 0;







        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPChunkQueue()
            : this(DefaultMaximumSize)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="maximumOfChunks">the maximum of packets allowed</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public RTSPChunkQueue(int maximumOfChunks)
        {
            _maximumOfChunks = maximumOfChunks > 0 ? maximumOfChunks : throw new ArgumentOutOfRangeException(nameof(maximumOfChunks));
            _lock = new object();
            _queue = new Queue<byte[]>(DefaultMaximumSize);
            _handle = new RTSPEventWaitHandle();
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
        /// Gets the maximum of chunks
        /// </summary>
        public int MaximumOfChunks
        {
            get
			{
                lock ( _lock )
				{
                    return _maximumOfChunks;
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
        public IEnumerator<byte[]> GetEnumerator()
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
        /// Post an element
        /// </summary>
        /// <param name="chunk">the event args</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool Enqueue(byte[] chunk )
        {
            if ( chunk == null || chunk.Length <= 0 )
			{
                return false;
			}

            lock ( _lock )
            {
                while ( _queue.Count >= _maximumOfChunks )
                {
                    _queue.Dequeue();
                }

                _queue.Enqueue( chunk );

                UpdateStatusHandle();

                return true;
            }
        }

        /// <summary>
        /// Dequeue a packet
        /// </summary>
        /// <returns>returns an instance, otherwise null</returns>
        public byte[] Dequeue()
        {
            lock ( _lock )
            {
                var result = _queue.Count > 0 ? _queue.Dequeue() : null;
                
                UpdateStatusHandle();
                return result;
            }
        }

        /// <summary>
        /// Dequeue an element
        /// </summary>
        /// <param name="chunk">the chunk</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool Dequeue( out byte[] chunk )
        {
            chunk = null;

            lock ( _lock )
            {
                chunk = _queue.Count > 0 ? _queue.Dequeue() : null;

                UpdateStatusHandle();

                return chunk != null && chunk.Length > 0 ;
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
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool Wait( int timeout )
        {
            return _handle.Wait( timeout );
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
            int size = _queue.Count;

            if ( size > 0 )
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
