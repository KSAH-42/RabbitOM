using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal sealed class StringRtspDictionary : IReadOnlyDictionary<string,string> , IDictionary<string , string>
    {
        private readonly Dictionary<string,string> _collection = new Dictionary<string, string>( StringComparer.OrdinalIgnoreCase );


        public string this[string key]
        {
            get => _collection[ key ?? string.Empty ];
            set => _collection[ key ?? string.Empty ] = value;
        }




        IEnumerable<string> IReadOnlyDictionary<string , string>.Keys
        {
            get => _collection.Keys;
        }

        IEnumerable<string> IReadOnlyDictionary<string , string>.Values
        {
            get => _collection.Values;
        }

        public ICollection<string> Keys
        {
            get => _collection.Keys;
        }

        public ICollection<string> Values
        {
            get => _collection.Values;
        }

        public int Count
        {
            get => _collection.Count;
        }

        public bool IsReadOnly
        {
            get => false;
        }

        public void Add( string key , string value )
        {
            _collection.Add( key ?? string.Empty , value ?? string.Empty );
        }

        public void Add( KeyValuePair<string , string> item )
        {
            _collection.Add( item.Key ?? string.Empty , item.Value ?? string.Empty );
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
            return _collection.ContainsKey( key ?? string.Empty );
        }

        public void CopyTo( KeyValuePair<string , string>[] array , int arrayIndex )
        {
            _collection.ToList().CopyTo( array , arrayIndex );
        }

        public IEnumerator<KeyValuePair<string , string>> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public bool Remove( string key )
        {
            return _collection.Remove( key ?? string.Empty );
        }

        public bool Remove( KeyValuePair<string , string> item )
        {
            return _collection.Remove( item.Key ?? string.Empty );
        }

        public bool TryGetValue( string key , out string value )
        {
            return _collection.TryGetValue( key ?? string.Empty , out value );
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public bool TryAdd( string key , string value )
        {
            if ( string.IsNullOrWhiteSpace( key ) )
            {
                return false;
            }

            if ( _collection.ContainsKey( key ) )
            {
                return false;
            }

            _collection[ key ] = value ?? string.Empty;

            return true;
        }
    }
}
