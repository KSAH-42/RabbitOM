using System;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes
{
    public class RtspHeaderValueCollection<TValue> : ICollection<TValue> , IReadOnlyCollection<TValue>
        where TValue : class
    {
        private readonly List<TValue> _collection = new List<TValue>();






        public TValue this[ int index ]
        {
            get => _collection[ index ];
        }







        public int Count
        {
            get => _collection.Count;
        }

        public bool IsReadOnly
        {
            get => false;
        }








        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public void Add( TValue item )
        {
            if ( ! OnValidate( item ) )
            {
                throw new ArgumentNullException( nameof( item ) );
            }

            _collection.Add( item );
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Contains( TValue item )
        {
            return _collection.Contains( item );
        }

        public void CopyTo( TValue[] array , int arrayIndex )
        {
            _collection.CopyTo( array , arrayIndex );
        }

        public bool Remove( TValue item )
        {
            return _collection.Remove( item );
        }

        public bool RemoveAt( int index )
        {
            if ( index < 0 || index >= _collection.Count )
            {
                return false;
            }

            _collection.RemoveAt( index );

            return true;
        }

        public bool TryAdd( TValue item )
        {
            if ( ! OnValidate( item ) )
            {
                return false;
            }

            _collection.Add( item );

            return true;
        }








        protected virtual bool OnValidate( TValue value )
        {
            return ! object.ReferenceEquals( value , null );
        }
    }
}
