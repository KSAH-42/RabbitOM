using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes
{
    public sealed class MediaTypeWithQualityRtspHeaderValueCollection : IEnumerable , IEnumerable<MediaTypeWithQualityRtspHeaderValue> , ICollection<MediaTypeWithQualityRtspHeaderValue> , IReadOnlyCollection<MediaTypeWithQualityRtspHeaderValue>
    {
        private readonly List<MediaTypeWithQualityRtspHeaderValue> _collection = new List<MediaTypeWithQualityRtspHeaderValue>();



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

        public IEnumerator<MediaTypeWithQualityRtspHeaderValue> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public void Add( MediaTypeWithQualityRtspHeaderValue item )
        {
            if ( item == null )
            {
                throw new ArgumentNullException( nameof( item ) );
            }

            _collection.Add( item );
        }

        public bool TryAdd( MediaTypeWithQualityRtspHeaderValue item )
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

        public bool Contains( MediaTypeWithQualityRtspHeaderValue item )
        {
            return _collection.Contains( item );
        }

        public void CopyTo( MediaTypeWithQualityRtspHeaderValue[] array , int arrayIndex )
        {
            _collection.CopyTo( array , arrayIndex );
        }

        public bool Remove( MediaTypeWithQualityRtspHeaderValue item )
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

        public bool RemoveBy( Func<MediaTypeWithQualityRtspHeaderValue,bool> predicate )
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
