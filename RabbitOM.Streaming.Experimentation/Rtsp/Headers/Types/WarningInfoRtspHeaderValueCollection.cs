using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    public sealed class WarningInfoRtspHeaderValueCollection : IEnumerable , IEnumerable<WarningInfoRtspHeaderValue> , ICollection<WarningInfoRtspHeaderValue> , IReadOnlyCollection<WarningInfoRtspHeaderValue>
    {
        private readonly List<WarningInfoRtspHeaderValue> _collection = new List<WarningInfoRtspHeaderValue>();





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

        public IEnumerator<WarningInfoRtspHeaderValue> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public void Add( WarningInfoRtspHeaderValue item )
        {
            if ( item == null )
            {
                throw new ArgumentNullException( nameof( item ) );
            }

            _collection.Add( item );
        }

        public bool TryAdd( WarningInfoRtspHeaderValue item )
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

        public bool Contains( WarningInfoRtspHeaderValue item )
        {
            return _collection.Contains( item );
        }

        public void CopyTo( WarningInfoRtspHeaderValue[] array , int arrayIndex )
        {
            _collection.CopyTo( array , arrayIndex );
        }

        public bool Remove( WarningInfoRtspHeaderValue item )
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

        public bool RemoveBy( Func<WarningInfoRtspHeaderValue,bool> predicate )
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
