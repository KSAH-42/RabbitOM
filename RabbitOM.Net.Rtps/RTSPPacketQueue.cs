using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RabbitOM.Net.Rtps
{
    /// <summary>
    /// Represent a packet circular queue
    /// </summary>
    public sealed class RTSPPacketQueue : IEnumerable<RTSPPacket>
    {
        /// <summary>
        /// The default packets numbers
        /// </summary>
        public const int                     DefaultMaximumOfPackets  = 32000;



        private readonly object              _lock              = null;

        private readonly RTSPEventWaitHandle _handle            = null;

        private readonly Queue<RTSPPacket>   _queue             = null;

        private readonly int                 _maximumOfpackets  = 0;







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
            _queue = new Queue<RTSPPacket>( maximumOfpackets );
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
        public IEnumerator<RTSPPacket> GetEnumerator()
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
        /// <param name="packet">the event args</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool Enqueue( RTSPPacket packet )
        {
            if ( packet == null || !packet.Validate() )
            {
                return false;
            }

            lock ( _lock )
            {
                while ( _queue.Count >= _maximumOfpackets )
                {
                    _queue.Dequeue();
                }

                _queue.Enqueue( packet );

                UpdateStatusHandle();

                return true;
            }
        }

        /// <summary>
        /// Dequeue a packet
        /// </summary>
        /// <returns>returns an instance, otherwise null</returns>
        public RTSPPacket Dequeue()
        {
            lock ( _lock )
            {
                RTSPPacket result = null;

                if ( _queue.Count > 0 )
                {
                    result = _queue.Dequeue();
                }

                UpdateStatusHandle();

                return result ?? RTSPPacket.Null;
            }
        }

        /// <summary>
        /// Dequeue an element
        /// </summary>
        /// <param name="packet">the packet</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool Dequeue( out RTSPPacket packet )
        {
            packet = null;

            lock ( _lock )
            {
                if ( _queue.Count > 0 )
                {
                    packet = _queue.Dequeue();
                }

                UpdateStatusHandle();

                return packet != null;
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
            if ( _queue.Count > 0 )
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
