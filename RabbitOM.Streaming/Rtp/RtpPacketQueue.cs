// refactorization: this class may need to used an internal priority queue
// without using the sort method

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp
{
    /// <summary>
    /// Represent a packet queue (not thread safe)
    /// </summary>
    public sealed class RtpPacketQueue : IEnumerable , IEnumerable<RtpPacket> , IReadOnlyCollection<RtpPacket>
    {
        private readonly Queue<RtpPacket> _collection;

        private uint? _lastSequenceNumber;

        private bool _canSort;






        /// <summary>
        /// Initialize a new instance of the packet queue
        /// </summary>
        public RtpPacketQueue()
        {
            _collection = new Queue<RtpPacket>();
        }

        /// <summary>
        /// Initialize a new instance of the packet queue
        /// </summary>
        /// <param name="capacity">the capacity</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public RtpPacketQueue( int capacity )
        {
            _collection = new Queue<RtpPacket>( capacity );
        }

        /// <summary>
        /// Initialize a new instance of the packet queue
        /// </summary>
        /// <param name="collection">the collection</param>
        /// <exception cref="ArgumentNullException"/>
        public RtpPacketQueue( IEnumerable<RtpPacket> collection )
        {
            if ( collection == null )
            {
                throw new ArgumentNullException( nameof( collection ) );
            }

            _collection = new Queue<RtpPacket>();

            foreach ( var element in collection )
            {
                _collection.Enqueue( element ?? throw new ArgumentNullException( "Bad element" , nameof( collection ) ) );
            }
        }








        /// <summary>
        /// Gets the number of elements
        /// </summary>
        public int Count
        {
            get => _collection.Count;
        }

        /// <summary>
        /// Check if the collection empty
        /// </summary>
        public bool IsEmpty
        {
            get => _collection.Count == 0;
        }









        /// <summary>
        /// Check if the
        /// </summary>
        /// <param name="queue"></param>
        /// <returns></returns>
        public static bool CanSort( RtpPacketQueue queue )
        {
            return queue != null && queue._canSort;
        }

        /// <summary>
        /// Sort the packet queue by sequence number
        /// </summary>
        /// <param name="queue">the queue to be sorted</param>
        /// <returns>returns an new instance</returns>
        /// <exception cref="ArgumentNullException"/>
        public static IEnumerable<RtpPacket> Sort( RtpPacketQueue queue )
        {
            if ( queue == null )
            {
                throw new ArgumentNullException( nameof( queue ) );
            }

            queue._canSort = false;

            return new Queue<RtpPacket>( queue.OrderBy( packet => packet.SequenceNumber ) );
        }











        /// <summary>
        /// Gets an enumerator
        /// </summary>
        /// <returns>returns an instance</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        /// <summary>
        /// Gets an enumerator
        /// </summary>
        /// <returns>returns an instance</returns>
        public IEnumerator<RtpPacket> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        /// <summary>
        /// Check if the collection has elements
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Any()
        {
            return _collection.Count > 0;
        }

        /// <summary>
        /// Check if the collection contains a particular elements
        /// </summary>
        /// <param name="packet">the packet</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Contains( RtpPacket packet )
        {
            return _collection.Contains( packet );
        }

        /// <summary>
        /// Clear the collection
        /// </summary>
        public void Clear()
        {
            _collection.Clear();

            OnClear();
        }

        /// <summary>
        /// Create an array of elements
        /// </summary>
        /// <returns>returns an array</returns>
        public RtpPacket[] ToArray()
        {
            return _collection.ToArray();
        }

        /// <summary>
        /// Create an enumerable of elements
        /// </summary>
        /// <returns>returns an array</returns>
        public IEnumerable<RtpPacket> AsEnumerable()
        {
            return new Queue<RtpPacket>( _collection );
        }

        /// <summary>
        /// Enqueue an element
        /// </summary>
        /// <param name="packet">the packet</param>
        /// <exception cref="ArgumentNullException"/>
        public void Enqueue( RtpPacket packet )
        {
            _collection.Enqueue( packet ?? throw new ArgumentNullException( nameof( packet ) ) );

            OnEnqueue( packet );
        }

        /// <summary>
        /// Dequeue a packet
        /// </summary>
        /// <returns>returns an instance, otherwise it throw an exception</returns>
        /// <exception cref="InvalidOperationException"/>
        public RtpPacket Dequeue()
        {
            var result = _collection.Dequeue() ?? throw new InvalidOperationException();

            OnDequeue( result );

            return result;
        }

        /// <summary>
        /// Peek a packet
        /// </summary>
        /// <returns>returns an instance, otherwise it thrown an exception</returns>
        /// <exception cref="InvalidOperationException"/>
        public RtpPacket Peek()
        {
            return _collection.Peek() ?? throw new InvalidOperationException();
        }

        /// <summary>
        /// Try to enqueue an element
        /// </summary>
        /// <param name="packet">the packet</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryEnqueue( RtpPacket packet )
        {
            if ( packet == null )
            {
                return false;
            }

            _collection.Enqueue( packet );

            OnEnqueue( packet );

            return true;
        }

        /// <summary>
        /// Try to dequeue an element
        /// </summary>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryDequeue( out RtpPacket result )
        {
            result = _collection.Count > 0 ? _collection.Dequeue() : null;

            if ( result == null )
            {
                return false;
            }

            OnDequeue( result );

            return true;
        }

        /// <summary>
        /// Try to peek an element
        /// </summary>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryPeek( out RtpPacket result )
        {
            result = _collection.Count > 0 ? _collection.Peek() : null ;
            
            return result != null;
        }






        /// <summary>
        /// Occurs when a packet is queued
        /// </summary>
        /// <param name="packet">the packet</param>
        private void OnEnqueue( RtpPacket packet )
        {
            if ( ! _lastSequenceNumber.HasValue )
            {
                _lastSequenceNumber = packet.SequenceNumber;
            }
            else
            {
                if ( ! _canSort )
                {
                    _canSort = _lastSequenceNumber > packet.SequenceNumber ? true : false;
                }

                _lastSequenceNumber = packet.SequenceNumber;
            }
        }

        /// <summary>
        /// Occurs when a packet is dequeue
        /// </summary>
        /// <param name="packet">the packet</param>
        private void OnDequeue( RtpPacket packet )
        {
            if ( _collection.Count == 0 )
            {
                _lastSequenceNumber = null;
                _canSort = false;
            }
        }

        /// <summary>
        /// Occurs when clear method is called
        /// </summary>
        private void OnClear()
        {
            _lastSequenceNumber = null;
            _canSort = false;
        }
    }
}