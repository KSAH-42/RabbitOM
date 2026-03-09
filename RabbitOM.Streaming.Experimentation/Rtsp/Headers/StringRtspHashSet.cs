using System;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal sealed class StringRtspHashSet : IEnumerable , IEnumerable<string> , ICollection<string> , IReadOnlyCollection<string> , ISet<string>
    {
        private readonly HashSet<string> _collection = new HashSet<string>( StringComparer.OrdinalIgnoreCase );







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

        public bool Contains( string item )
        {
            return _collection.Contains( item );
        }

        void ICollection<string>.Add( string item )
        {
            if ( string.IsNullOrWhiteSpace( item ) )
            {
                return;
            }

            _collection.Add( item );
        }

        public bool Add( string item )
        {
            if ( string.IsNullOrWhiteSpace( item ) )
            {
                return false;
            }

            return _collection.Add( item );
        }

        public bool Remove( string item )
        {
            return _collection.Remove( item );
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public void CopyTo( string[] array , int arrayIndex )
        {
            _collection.CopyTo( array , arrayIndex );
        }

        public void ExceptWith( IEnumerable<string> other )
        {
            _collection.ExceptWith( other );
        }

        public void IntersectWith( IEnumerable<string> other )
        {
            _collection.IntersectWith( other );
        }

        public bool IsProperSubsetOf( IEnumerable<string> other )
        {
            return _collection.IsProperSubsetOf( other );
        }

        public bool IsProperSupersetOf( IEnumerable<string> other )
        {
            return _collection.IsProperSupersetOf( other );
        }

        public bool IsSubsetOf( IEnumerable<string> other )
        {
            return _collection.IsSubsetOf( other );
        }

        public bool IsSupersetOf( IEnumerable<string> other )
        {
            return _collection.IsSubsetOf( other );
        }

        public bool Overlaps( IEnumerable<string> other )
        {
            return _collection.Overlaps( other );
        }

        public bool SetEquals( IEnumerable<string> other )
        {
            return _collection.SetEquals( other );
        }

        public void SymmetricExceptWith( IEnumerable<string> other )
        {
            _collection.SymmetricExceptWith( other );
        }

        public void UnionWith( IEnumerable<string> other )
        {
            _collection.UnionWith( other );
        }
    }
}
