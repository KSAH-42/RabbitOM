using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a packet circular queue
    /// </summary>
    public sealed partial class RTSPPacketQueue : IEnumerable<RTSPPacket>
    {
        /// <summary>
        /// The default packets numbers
        /// </summary>
        public const int                     DefaultMaximumOfPackets  = 32000;



        private readonly object              _lock              = null;

        private readonly RTSPEventWaitHandle _handle            = null;

        private readonly Queue<RTSPPacket>   _collection             = null;

        private readonly int                 _maximumOfpackets  = 0;

        private readonly Scope               _scope             = null;







        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPPacketQueue()
            : this( DefaultMaximumOfPackets )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="maximumOfpackets">the maximum of packets allowed</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public RTSPPacketQueue( int maximumOfpackets )
        {
            _maximumOfpackets = maximumOfpackets <= 0 ? maximumOfpackets : throw new ArgumentOutOfRangeException( nameof( maximumOfpackets ) );
            _lock = new object();
            _collection = new Queue<RTSPPacket>( maximumOfpackets );
            _handle = new RTSPEventWaitHandle();
            _scope = new Scope( this );
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
        public static bool Wait(RTSPPacketQueue queue)
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
        public static bool Wait(RTSPPacketQueue queue, int timeout)
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
        public static bool Wait(RTSPPacketQueue queue, EventWaitHandle cancellationHandle)
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
        public static bool Wait(RTSPPacketQueue queue, int timeout, EventWaitHandle cancellationHandle)
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
        public IEnumerator<RTSPPacket> GetEnumerator()
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
        /// <param name="packet">the event args</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool Enqueue( RTSPPacket packet )
        {
            if ( packet == null || ! packet.TryValidate() )
            {
                return false;
            }

            lock ( _lock )
            {
                using ( _scope )
                {
                    while (_collection.Count >= _maximumOfpackets)
                    {
                        _collection.Dequeue();
                    }

                    _collection.Enqueue(packet);

                    return true;
                }
            }
        }

        /// <summary>
        /// Dequeue a packet
        /// </summary>
        /// <returns>must returns an instance</returns>
        public RTSPPacket Dequeue()
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
        /// <param name="packet">the packet</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool TryDequeue( out RTSPPacket packet )
        {
            lock ( _lock )
            {
                using ( _scope )
                {
                    packet = _collection.Count > 0 ? _collection.Dequeue() : null;
                    
                    return packet != null;
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
