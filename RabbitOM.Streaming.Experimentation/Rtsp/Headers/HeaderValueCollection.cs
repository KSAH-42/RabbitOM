using System;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class HeaderValueCollection<TValue> : IEnumerable, IEnumerable<TValue>, ICollection, ICollection<TValue>
    {
        private readonly List<TValue> _collection = new List<TValue>();

        private readonly Func<TValue,bool> _validator;
        




        public HeaderValueCollection()
        {
        }

        public HeaderValueCollection( Func<TValue,bool> validator )
        {
            _validator = validator ?? throw new ArgumentNullException( nameof( validator ) );
        }





        public object SyncRoot
        {
            get => _collection;
        }

        public int Count
        {
            get => _collection.Count;
        }

        public bool IsEmpty
        {
            get => _collection.Count == 0;
        }

        public bool IsSynchronized
        {
            get => false;
        }

        public bool IsReadOnly
        {
            get => false;
        }

        public void Add( TValue item )
        {
            if ( item == null )
            {
                throw new ArgumentNullException( nameof( item ) );
            }

            if ( _validator != null && ! _validator( item ) )
            {
                throw new ArgumentException( nameof( item ) );
            }

            _collection.Add( item );
        }

        public bool TryAdd( TValue item )
        {
            if ( item == null )
            {
                return false;
            }

            if ( _validator != null && ! _validator( item ) )
            {
                return false;
            }

            _collection.Add( item );

            return true;
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Contains( TValue item )
        {
            return _collection.Contains( item );
        }

        public void CopyTo( Array array , int index )
        {
            _collection.CopyTo( array as TValue[] , index );
        }

        public void CopyTo( TValue[] array , int arrayIndex )
        {
            _collection.CopyTo( array , arrayIndex );
        }

        public bool Remove( TValue item )
        {
            return _collection.Remove( item );
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join( ", " , _collection );
        }
    }
}
