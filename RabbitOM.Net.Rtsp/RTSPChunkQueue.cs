using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a chunk circular queue
    /// </summary>
    internal sealed partial class RTSPChunkQueue : IEnumerable , IEnumerable<byte[]>
    {
        /// <summary>
        /// The default maximum size
        /// </summary>
        public const int                     DefaultMaximumSize = 32000;




        private readonly object              _lock              = null;

        private readonly RTSPEventWaitHandle _handle            = null;

        private readonly Queue<byte[]>       _collection        = null;

        private readonly int                 _maximumOfChunks   = 0;

        private readonly Scope               _scope             = null;







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
            
            _lock            = new object();
            _collection      = new Queue<byte[]>(DefaultMaximumSize);
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
        public static bool Wait(RTSPChunkQueue queue)
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
        public static bool Wait(RTSPChunkQueue queue, int timeout)
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
        public static bool Wait(RTSPChunkQueue queue, EventWaitHandle cancellationHandle)
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
        public static bool Wait(RTSPChunkQueue queue, int timeout, EventWaitHandle cancellationHandle)
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
        public IEnumerator<byte[]> GetEnumerator()
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
        /// <param name="chunk">the chunk</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool Enqueue(byte[] chunk )
        {
            if ( chunk == null || chunk.Length <= 0 )
			{
                return false;
			}
        
            lock ( _lock )
            {
                using (_scope )
                {
                    while (_collection.Count >= _maximumOfChunks)
                    {
                        _collection.Dequeue();
                    }

                    _collection.Enqueue(chunk);

                    return true;
                }
            }
        }

        /// <summary>
        /// Dequeue a chunk
        /// </summary>
        /// <returns>must returns an instance</returns>
        public byte[] Dequeue()
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
        /// Dequeue an element
        /// </summary>
        /// <param name="result">the chunk</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool TryDequeue( out byte[] result )
        {
            lock ( _lock )
            {
                using ( _scope )
                {
                    result = _collection.Count > 0 ? _collection.Dequeue() : null;

                    return result != null && result.Length > 0 ;
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
