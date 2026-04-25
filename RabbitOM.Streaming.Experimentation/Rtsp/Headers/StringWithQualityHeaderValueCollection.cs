using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Normalizers;

    public sealed class StringWithQualityHeaderValueCollection : ICollection<StringWithQualityHeaderValue>
    {
        private readonly List<StringWithQualityHeaderValue> _collection = new List<StringWithQualityHeaderValue>();




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

        public IEnumerator<StringWithQualityHeaderValue> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public void Add( StringWithQualityHeaderValue item )
        {
            if ( item == null )
            {
                throw new ArgumentNullException( nameof( item ) );
            }
            
            _collection.Add( item );
        }

        public bool TryAdd( StringWithQualityHeaderValue item )
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

        public bool Contains( StringWithQualityHeaderValue item )
        {
            return _collection.Contains( item );
        }

        public void CopyTo( StringWithQualityHeaderValue[] array , int arrayIndex )
        {
            _collection.CopyTo( array , arrayIndex );
        }

        public bool Remove( StringWithQualityHeaderValue item )
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

        public bool RemoveBy( Func<StringWithQualityHeaderValue,bool> predicate )
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

        public override string ToString()
        {
            return string.Join( ", " , _collection );
        }

        public static bool TryParse( string input , StringValueNormalizer normaliser , out StringWithQualityHeaderValueCollection result )
        {
            result = null;

            if ( HeaderParser.TryParse( input , "," , out string[] tokens ) )
            {
                var collection = new StringWithQualityHeaderValueCollection();

                foreach( var token in tokens )
                {
                    if ( StringWithQualityHeaderValue.TryParse( normaliser?.Normalize( token ) ?? token , out var method ) )
                    {
                        collection.TryAdd( method );
                    }
                }

                if ( collection.Count > 0 )
                {
                    result = collection;
                }
            }

            return result != null;
        }
    }
}
