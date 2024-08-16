using System;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265ElementQueue : IEnumerable , IEnumerable<H265Element> , IReadOnlyCollection<H265Element>
    {
        private readonly Queue<H265Element> _collection;





        public H265ElementQueue()
        {
            _collection = new Queue<H265Element>();
        }

        public H265ElementQueue( int capacity )
        {
            _collection = new Queue<H265Element>( capacity );
        }

        public H265ElementQueue( IEnumerable<H265Element> collection )
        {
            if ( collection == null )
            {
                throw new ArgumentNullException( nameof( collection ) );
            }

            _collection = new Queue<H265Element>();

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

        public IEnumerator<H265Element> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public bool Any()
        {
            return _collection.Count > 0;
        }

        public bool Contains( H265Element nalUnit )
        {
            return _collection.Contains( nalUnit );
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public H265Element[] ToArray()
        {
            return _collection.ToArray();
        }

        public void Enqueue( H265Element nalUnit )
        {
            _collection.Enqueue( nalUnit ?? throw new ArgumentNullException( nameof( nalUnit ) ) );
        }

        public H265Element Dequeue()
        {
            return _collection.Dequeue() ?? throw new InvalidOperationException();
        }

        public H265Element Peek()
        {
            return _collection.Peek() ?? throw new InvalidOperationException();
        }

        public bool TryEnqueue( H265Element nalUnit )
        {
            if ( nalUnit == null )
            {
                return false;
            }

            _collection.Enqueue( nalUnit );

            return true;
        }

        public bool TryDequeue( out H265Element result )
        {
            result = _collection.Count > 0 ? _collection.Dequeue() : null;

            return result != null;
        }

        public bool TryPeek( out H265Element result )
        {
            result = _collection.Count > 0 ? _collection.Peek() : null ;
            
            return result != null;
        }
    }
}