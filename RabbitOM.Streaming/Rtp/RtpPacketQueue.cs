using System;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp
{
    // Need to be thread safe ?
    // Later, this collection will be used a priorty queue to handle reording packet, this is not fondamental, but in some rare cases it can happen

    public sealed class RtpPacketQueue : IEnumerable , IEnumerable<RtpPacket> , IReadOnlyCollection<RtpPacket>
    {
        private readonly Queue<RtpPacket> _collection;





        public RtpPacketQueue()
        {
            _collection = new Queue<RtpPacket>();
        }

        public RtpPacketQueue( int capacity )
        {
            _collection = new Queue<RtpPacket>( capacity );
        }

        public RtpPacketQueue( IEnumerable<RtpPacket> collection )
        {
            if ( collection == null )
            {
                throw new ArgumentNullException( nameof( collection ) );
            }

            _collection = new Queue<RtpPacket>();  // avoid a filter using linq and pass the result on the constuctor for performance reason, look using reflector

            foreach ( var element in collection )
            {
                _collection.Enqueue( element ?? throw new ArgumentNullException( "Bad element" , nameof( collection ) ) );
            }
        }






        public int Count
        {
            get => _collection.Count;
        }

        public bool IsEmpty
        {
            get => _collection.Count == 0;
        }







        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public IEnumerator<RtpPacket> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public bool Any()
        {
            return _collection.Count > 0;
        }

        public bool Contains( RtpPacket packet )
        {
            return _collection.Contains( packet );
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public RtpPacket[] ToArray()
        {
            return _collection.ToArray();
        }

        public void Enqueue( RtpPacket packet )
        {
            _collection.Enqueue( packet ?? throw new ArgumentNullException( nameof( packet ) ) );
        }

        public RtpPacket Dequeue()
        {
            return _collection.Dequeue() ?? throw new InvalidOperationException();
        }

        public RtpPacket Peek()
        {
            return _collection.Peek() ?? throw new InvalidOperationException();
        }

        public bool TryEnqueue( RtpPacket packet )
        {
            if ( packet == null )
            {
                return false;
            }

            _collection.Enqueue( packet );

            return true;
        }

        public bool TryDequeue( out RtpPacket result )
        {
            result = _collection.Count > 0 ? _collection.Dequeue() : null;

            return result != null;
        }

        public bool TryPeek( out RtpPacket result )
        {
            result = _collection.Count > 0 ? _collection.Peek() : null ;
            
            return result != null;
        }
    }
}