using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    public sealed class StringParameterRtspHeaderValueCollection : IEnumerable , IEnumerable<KeyValuePair<string,string>>
    {
        private readonly Dictionary<string,string> _collection = new Dictionary<string, string>( StringComparer.OrdinalIgnoreCase );



        
        
        
        public int Count
        {
            get => _collection.Count;
        }

        public bool IsReadOnly
        {
            get => false;
        }

        public ICollection<string> Keys
        {
            get => _collection.Keys;
        }

        



        public void Add( string name , string value )
        {
            if ( ! RtspHeaderValueValidator.TryEnsureWellFormedToken( name ) )
            {
                throw new ArgumentException( nameof( name ) );
            }

            if ( value?.Length > 0 && ! RtspHeaderValueValidator.TryEnsureWellFormedToken( value ) )
            {
                throw new ArgumentException();
            }

            if ( _collection.ContainsKey( name ) )
            {
                throw new InvalidOperationException( nameof( name ) );
            }

            _collection[ name ] = value ?? string.Empty;
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Contains( KeyValuePair<string , string> item )
        {
            return  item.Key != null && _collection.Contains( item );
        }

        public bool ContainsKey( string key )
        {
            return _collection.ContainsKey( key ?? string.Empty );
        }

        public void CopyTo( KeyValuePair<string , string>[] array , int arrayIndex )
        {
            _collection.ToList().CopyTo( array , arrayIndex );
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public IEnumerator<KeyValuePair<string , string>> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public bool Remove( string name )
        {
            return _collection.Remove( name ?? string.Empty );
        }

        public bool TryAdd( string name , string value )
        {
            if ( ! RtspHeaderValueValidator.TryEnsureWellFormedToken( name ) )
            {
                return false;
            }

            if ( value?.Length > 0 && ! RtspHeaderValueValidator.TryEnsureWellFormedToken( value ) )
            {
                return false;
            }

            _collection[ name ] = value ?? string.Empty;

            return true;
        }

        public bool TryGetValue( string key , out string value )
        {
            return _collection.TryGetValue( key ?? string.Empty , out value );
        }
    }
}
