using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    public sealed class WarningInfoRtspHeaderValueCollection : IEnumerable , IEnumerable<WarningInfo> , ICollection<WarningInfo> , IReadOnlyCollection<WarningInfo>
    {
        private readonly List<WarningInfo> _collection = new List<WarningInfo>();





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

        public IEnumerator<WarningInfo> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public void Add( WarningInfo item )
        {
            if ( item == null )
            {
                throw new ArgumentNullException( nameof( item ) );
            }

            _collection.Add( item );
        }

        public bool TryAdd( WarningInfo item )
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

        public bool Contains( WarningInfo item )
        {
            return _collection.Contains( item );
        }

        public void CopyTo( WarningInfo[] array , int arrayIndex )
        {
            _collection.CopyTo( array , arrayIndex );
        }

        public bool Remove( WarningInfo item )
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

        public bool RemoveBy( Func<WarningInfo,bool> predicate )
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
