using System;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265NalUnitQueue : IEnumerable , IEnumerable<H265NalUnit> , IReadOnlyCollection<H265NalUnit>
    {
        private readonly Queue<H265NalUnit> _collection;





        public H265NalUnitQueue()
        {
            _collection = new Queue<H265NalUnit>();
        }

        public H265NalUnitQueue( int capacity )
        {
            _collection = new Queue<H265NalUnit>( capacity );
        }

        public H265NalUnitQueue( IEnumerable<H265NalUnit> collection )
        {
            if ( collection == null )
            {
                throw new ArgumentNullException( nameof( collection ) );
            }

            _collection = new Queue<H265NalUnit>();

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

        public IEnumerator<H265NalUnit> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public bool Any()
        {
            return _collection.Count > 0;
        }

        public bool Contains( H265NalUnit nalUnit )
        {
            return _collection.Contains( nalUnit );
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public H265NalUnit[] ToArray()
        {
            return _collection.ToArray();
        }

        public void Enqueue( H265NalUnit nalUnit )
        {
            _collection.Enqueue( nalUnit ?? throw new ArgumentNullException( nameof( nalUnit ) ) );
        }

        public H265NalUnit Dequeue()
        {
            return _collection.Dequeue() ?? throw new InvalidOperationException();
        }

        public H265NalUnit Peek()
        {
            return _collection.Peek() ?? throw new InvalidOperationException();
        }

        public bool TryEnqueue( H265NalUnit nalUnit )
        {
            if ( nalUnit == null )
            {
                return false;
            }

            _collection.Enqueue( nalUnit );

            return true;
        }

        public bool TryDequeue( out H265NalUnit result )
        {
            result = _collection.Count > 0 ? _collection.Dequeue() : null;

            return result != null;
        }

        public bool TryPeek( out H265NalUnit result )
        {
            result = _collection.Count > 0 ? _collection.Peek() : null ;
            
            return result != null;
        }
    }
}