using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Normalizers;

    public sealed class MethodHeaderValueCollection : ICollection<MethodHeaderValue>
    {
        private readonly Dictionary<string,MethodHeaderValue> _collection = new Dictionary<string, MethodHeaderValue>( StringComparer.OrdinalIgnoreCase );




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

        public IEnumerator<MethodHeaderValue> GetEnumerator()
        {
            return _collection.Values.GetEnumerator();
        }

        public void Add( MethodHeaderValue item )
        {
            if ( item == null )
            {
                throw new ArgumentNullException( nameof( item ) );
            }

            _collection.Add( item.Name , item );
        }

        public bool TryAdd( MethodHeaderValue item )
        {
            if ( item == null )
            {
                return false;
            }

            if ( _collection.ContainsValue( item ) || _collection.ContainsKey( item.Name ) )
            {
                return false;
            }

            _collection.Add( item.Name , item );

            return true;
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Contains( MethodHeaderValue item )
        {
            return _collection.Values.Contains( item );
        }

        public bool Contains( string name )
        {
            return _collection.ContainsKey( name );
        }

        public void CopyTo( MethodHeaderValue[] array , int arrayIndex )
        {
            _collection.Values.CopyTo( array , arrayIndex );
        }

        public bool Remove( MethodHeaderValue item )
        {
            return _collection.ContainsValue( item );
        }

        public bool RemoveByName( string name )
        {
            return _collection.ContainsKey( name );
        }

        public bool RemoveBy( Func<MethodHeaderValue,bool> predicate )
        {
            if ( predicate == null )
            {
                throw new ArgumentNullException( nameof( predicate ) );
            }

            var item = _collection.Values.FirstOrDefault( predicate );

            if ( item == null )
            {
                return false;
            }

            return _collection.Remove( item.Name );
        }

        public override string ToString()
        {
            return string.Join( ", " , _collection.Values );
        }

        public static bool TryParse( string input , StringValueNormalizer normaliser , out MethodHeaderValueCollection result )
        {
            result = null;

            if ( HeaderParser.TryParse( input , "," , out string[] tokens ) )
            {
                var collection = new MethodHeaderValueCollection();

                foreach( var token in tokens )
                {
                    if ( MethodHeaderValue.TryParse( normaliser?.Normalize( token ) ?? token , out var method ) )
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
