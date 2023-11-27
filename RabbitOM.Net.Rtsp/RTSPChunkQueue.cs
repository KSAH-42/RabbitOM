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
    internal sealed class RTSPChunkQueue : RTSPBaseQueue<byte[]>
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
        protected override RTSPEventWaitHandle Handle
        {
            get => _handle;
        }







        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>returns an enumerator</returns>
        protected override IEnumerator<byte[]> BaseGetEnumerator()
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
        /// Post an element
        /// </summary>
        /// <param name="chunk">the chunk</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public override bool Enqueue(byte[] chunk )
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
        public override byte[] Dequeue()
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
        public override bool TryDequeue( out byte[] result )
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
