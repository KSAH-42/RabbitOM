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
    internal sealed class RTSPPacketQueue : RTSPBaseQueue<RTSPPacket>
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
        protected override IEnumerator<RTSPPacket> BaseGetEnumerator()
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
        /// Enqueue a packet
        /// </summary>
        /// <param name="packet">the packet</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public override bool Enqueue( RTSPPacket packet )
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
        public override RTSPPacket Dequeue()
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
        /// <param name="result">the packet</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public override bool TryDequeue( out RTSPPacket result )
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
