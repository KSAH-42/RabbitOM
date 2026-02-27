using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class RtspHeaderValueDictionary : IDictionary<string , RtspHeaderValue>
    {
        private readonly Dictionary<string,RtspHeaderValue> _collection = new Dictionary<string, RtspHeaderValue>( StringComparer.OrdinalIgnoreCase );

        public RtspHeaderValue this[string key]
        {
            get => _collection[ key ];
            set => _collection[ string.IsNullOrWhiteSpace( key ) ? throw new ArgumentException( key ) : key ] = value ?? throw new ArgumentNullException( nameof( value ) );
        }

        public ICollection<string> Keys
        {
            get => _collection.Keys;
        }

        public ICollection<RtspHeaderValue> Values
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

        public bool TryAdd( string key , RtspHeaderValue value )
        {
            if ( string.IsNullOrWhiteSpace( key ) || value == null )
            {
                return false;
            }
            
            if ( _collection.ContainsKey( key ) )
            {
                return false;
            }

            _collection[ key ] = value;

            return true;
        }

        public void Add( string key , RtspHeaderValue value )
        {
            if ( string.IsNullOrWhiteSpace( key ) )
            {
                throw key == null ? new ArgumentNullException( nameof( key ) ) : new ArgumentException( nameof( key ) );
            }
            
            _collection.Add( key , value ?? throw new ArgumentNullException( nameof( key ) ) );
        }

        public void Add( KeyValuePair<string , RtspHeaderValue> item )
        {
            Add( item.Key , item.Value );
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Contains( KeyValuePair<string , RtspHeaderValue> item )
        {
            return _collection.Contains( item );
        }

        public bool ContainsKey( string key )
        {
            return _collection.ContainsKey( key );
        }

        public void CopyTo( KeyValuePair<string , RtspHeaderValue>[] array , int arrayIndex )
        {
            _collection.ToList().CopyTo( array , arrayIndex );
        }

        public bool Remove( string key )
        {
            return _collection.Remove( key );
        }

        public bool Remove( KeyValuePair<string , RtspHeaderValue> item )
        {
            if ( ! _collection.TryGetValue( item.Key , out var header ) )
            {
                return false;
            }

            if ( object.ReferenceEquals( item.Value , header ) || item.Value?.Equals( header ) == true )
            {
                return _collection.Remove( item.Key );
            }

            return false;
        }

        public bool TryGetValue( string key , out RtspHeaderValue value )
        {
            return _collection.TryGetValue( key , out value );
        }

        public IEnumerator<KeyValuePair<string , RtspHeaderValue>> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }
    }
}
