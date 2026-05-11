using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    public sealed class StringRtspHeaderValueCollection : IEnumerable , IEnumerable<string> , ICollection<string> , IReadOnlyCollection<string>
    {
        private readonly List<string> _collection = new List<string>();




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

        public IEnumerator<string> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }       

        public void Add( string item )
        {
            if ( ! RtspHeaderValueValidator.TryEnsureWellFormedToken( item ) )
            {
                throw new ArgumentNullException( nameof( item ) );
            }

            _collection.Add( item );
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Contains( string item )
        {
            return _collection.Contains( item );
        }

        public void CopyTo( string[] array , int arrayIndex )
        {
            _collection.CopyTo( array , arrayIndex );
        }

        public bool Remove( string item )
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

        public bool RemoveBy( Func<string,bool> predicate )
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

        public bool TryAdd( string item )
        {
            if ( ! RtspHeaderValueValidator.TryEnsureWellFormedToken( item ) )
            {
                return false;
            }

            _collection.Add( item );

            return true;
        }
    }
}
