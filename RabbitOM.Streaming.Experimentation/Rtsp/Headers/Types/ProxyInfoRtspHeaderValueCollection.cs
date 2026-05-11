using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    public sealed class ProxyInfoRtspHeaderValueCollection : IEnumerable , IEnumerable<ProxyInfoRtspHeaderValue> , ICollection<ProxyInfoRtspHeaderValue> , IReadOnlyCollection<ProxyInfoRtspHeaderValue>
    {
        private readonly List<ProxyInfoRtspHeaderValue> _collection = new List<ProxyInfoRtspHeaderValue>();





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

        public IEnumerator<ProxyInfoRtspHeaderValue> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public void Add( ProxyInfoRtspHeaderValue item )
        {
            if ( item == null )
            {
                throw new ArgumentNullException( nameof( item ) );
            }

            _collection.Add( item );
        }

        public bool TryAdd( ProxyInfoRtspHeaderValue item )
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

        public bool Contains( ProxyInfoRtspHeaderValue item )
        {
            return _collection.Contains( item );
        }

        public void CopyTo( ProxyInfoRtspHeaderValue[] array , int arrayIndex )
        {
            _collection.CopyTo( array , arrayIndex );
        }

        public bool Remove( ProxyInfoRtspHeaderValue item )
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

        public bool RemoveBy( Func<ProxyInfoRtspHeaderValue,bool> predicate )
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
