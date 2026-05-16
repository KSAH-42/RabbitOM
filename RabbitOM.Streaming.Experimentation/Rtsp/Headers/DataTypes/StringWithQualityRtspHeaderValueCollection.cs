using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes
{
    public sealed class StringWithQualityRtspHeaderValueCollection : IEnumerable , IEnumerable<StringWithQualityRtspHeaderValue> , ICollection<StringWithQualityRtspHeaderValue> , IReadOnlyCollection<StringWithQualityRtspHeaderValue>
    {
        private readonly List<StringWithQualityRtspHeaderValue> _collection = new List<StringWithQualityRtspHeaderValue>();



        public StringWithQualityRtspHeaderValue this[ int index ]
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
            return GetEnumerator();
        }

        public IEnumerator<StringWithQualityRtspHeaderValue> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public void Add( StringWithQualityRtspHeaderValue item )
        {
            if ( item == null )
            {
                throw new ArgumentNullException( nameof( item ) );
            }

            _collection.Add( item );
        }

        public bool TryAdd( StringWithQualityRtspHeaderValue item )
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

        public bool Contains( StringWithQualityRtspHeaderValue item )
        {
            return _collection.Contains( item );
        }

        public void CopyTo( StringWithQualityRtspHeaderValue[] array , int arrayIndex )
        {
            _collection.CopyTo( array , arrayIndex );
        }

        public bool Remove( StringWithQualityRtspHeaderValue item )
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

        public bool RemoveBy( Func<StringWithQualityRtspHeaderValue,bool> predicate )
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
