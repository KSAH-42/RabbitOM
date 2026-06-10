using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes
{
    public sealed class RtpInfoSourceRtspHeaderValueCollection : IEnumerable , IEnumerable<RtpInfoSourceRtspHeaderValue> , ICollection<RtpInfoSourceRtspHeaderValue> , IReadOnlyCollection<RtpInfoSourceRtspHeaderValue>
    {
        private readonly List<RtpInfoSourceRtspHeaderValue> _collection = new List<RtpInfoSourceRtspHeaderValue>();




        public RtpInfoSourceRtspHeaderValue this[ int index ]
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

        public IEnumerator<RtpInfoSourceRtspHeaderValue> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public void Add( RtpInfoSourceRtspHeaderValue item )
        {
            if ( item == null )
            {
                throw new ArgumentNullException( nameof( item ) );
            }

            _collection.Add( item );
        }

        public bool TryAdd( RtpInfoSourceRtspHeaderValue item )
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

        public bool Contains( RtpInfoSourceRtspHeaderValue item )
        {
            return _collection.Contains( item );
        }

        public void CopyTo( RtpInfoSourceRtspHeaderValue[] array , int arrayIndex )
        {
            _collection.CopyTo( array , arrayIndex );
        }

        public bool Remove( RtpInfoSourceRtspHeaderValue item )
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

        public bool RemoveBy( Func<RtpInfoSourceRtspHeaderValue,bool> predicate )
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
