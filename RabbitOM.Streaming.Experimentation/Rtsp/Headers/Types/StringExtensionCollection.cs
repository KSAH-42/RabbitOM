using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Compliances;

    public sealed class StringExtensionCollection : IEnumerable , IEnumerable<KeyValuePair<string,string>>
    {
        private readonly INormalizer<string> _normalizer;

        private readonly Dictionary<string,string> _collection;
        private readonly Func<string,string,bool> _validator;

        public StringExtensionCollection( INormalizer<string> normalizer , Func<string,string,bool> validator = null )
        {
            _normalizer = normalizer ?? throw new ArgumentNullException( nameof( normalizer ) );
            _collection = new Dictionary<string, string>( StringComparer.OrdinalIgnoreCase );
            _validator = validator;
        }

        
        
        
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
            name = _normalizer.Normalize( name );

            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new ArgumentException( nameof( name ) );
            }

            if ( _validator != null && ! _validator( name , value ) )
            {
                throw new ArgumentException();
            }

            if ( _collection.ContainsKey( name ) )
            {
                throw new InvalidOperationException( nameof( name ) );
            }

            _collection[ name ] = _normalizer.Normalize( value );
        }

        public bool TryAdd( string name , string value )
        {
            name = _normalizer.Normalize( name );

            if ( string.IsNullOrWhiteSpace( name ) )
            {
                return false;
            }

            if ( _validator != null && ! _validator( name , value ) )
            {
                return false;
            }

            _collection[ name ] = _normalizer.Normalize( value );

            return true;
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Contains( KeyValuePair<string , string> item )
        {
            return _collection.Contains( item );
        }

        public bool ContainsKey( string key )
        {
            return _collection.ContainsKey( _normalizer.Normalize( key ) );
        }

        public void CopyTo( KeyValuePair<string , string>[] array , int arrayIndex )
        {
            _collection.ToList().CopyTo( array , arrayIndex );
        }

        public IEnumerator<KeyValuePair<string , string>> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public bool Remove( string name )
        {
            return _collection.Remove( _normalizer.Normalize( name ) );
        }

        public bool TryGetValue( string key , out string value )
        {
            return _collection.TryGetValue( _normalizer.Normalize( key ) , out value );
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }
    }
}
