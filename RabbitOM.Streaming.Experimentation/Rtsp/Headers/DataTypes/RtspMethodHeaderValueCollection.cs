using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes
{
    public sealed class RtspMethodHeaderValueCollection : IEnumerable , IEnumerable<RtspMethod> , ICollection<RtspMethod> , IReadOnlyCollection<RtspMethod>
    {
        private readonly HashSet<RtspMethod> _collection = new HashSet<RtspMethod>();





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
            return GetEnumerator();
        }

        public IEnumerator<RtspMethod> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public void Add( RtspMethod item )
        {
            if ( item == null )
            {
                throw new ArgumentNullException( nameof( item ) );
            }

            _collection.Add( item );
        }

        public bool TryAdd( RtspMethod item )
        {
            if ( item == null )
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

        public bool Contains( RtspMethod item )
        {
            return _collection.Contains( item );
        }

        public void CopyTo( RtspMethod[] array , int arrayIndex )
        {
            _collection.CopyTo( array , arrayIndex );
        }

        public bool Remove( RtspMethod item )
        {
            return _collection.Remove( item );
        }

        public bool RemoveAt( int index )
        {
            if ( index < 0 || index >= _collection.Count )
            {
                return false;
            }

            var element = _collection.ElementAtOrDefault( index );

            if ( element == null )
            {
                return false;
            }

            _collection.Remove( element );

            return true;
        }

        public bool RemoveBy( Func<RtspMethod,bool> predicate )
        {
            if ( predicate == null )
            {
                throw new ArgumentNullException( nameof( predicate ) );
            }
            
            var item = _collection.FirstOrDefault( predicate );

            if ( item == null )
            {
                return false;
            }

            return _collection.Remove( item );
        }
    }
}
