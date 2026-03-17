using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    // TODO: adding unit tests

    public sealed class InternalRtspHeaderCollection : IEnumerable , IEnumerable<KeyValuePair<string , string[]>> , IReadOnlyHeaderCollection
    {
        private readonly Dictionary<string,List<string>> _items = new Dictionary<string, List<string>>( StringComparer.OrdinalIgnoreCase );


        


        public string[] this[ string key ] 
        { 
            get => _items[ key ].ToArray();
        }

        public string this[ string key , int index ]
        {
            get => _items[ key ].ElementAt( index );
        }





        public int Count
        {
            get => _items.Count;
        }

        public string[] AllKeys
        {
            get => _items.Keys.ToArray();
        }
        public bool IsReadOnly
        {
            get => false;
        }





        public struct Enumerator : IEnumerator<KeyValuePair<string , string[]>>
        {
            private readonly IEnumerator<KeyValuePair<string,List<string>>> _enumerator;

            private KeyValuePair<string,string[]> _current;
            

            internal Enumerator( InternalRtspHeaderCollection collection )
            {
                _enumerator = collection._items.GetEnumerator();
                _current = default;
            }


            object IEnumerator.Current
            {
                get => _current;
            }

            public KeyValuePair<string , string[]> Current
            {
                get => _current;
            }


            public void Dispose()
            {
                _enumerator.Dispose();
            }

            public bool MoveNext()
            {
                if (  _enumerator.MoveNext() )
                {
                    _current = new KeyValuePair<string, string[]>( _enumerator.Current.Key , _enumerator.Current.Value.ToArray() );
                
                    return true;
                }

                return false;
            }

            public void Reset()
            {
                _enumerator.Reset();
                _current = default;
            }
        }





        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator( this );
        }

        public IEnumerator<KeyValuePair<string , string[]>> GetEnumerator()
        {
            return new Enumerator( this );
        }
                
        public void Add( string key , string value )
        {
            if ( key == null )
            {
                throw new ArgumentNullException( nameof( key ) );
            }

            if ( string.IsNullOrWhiteSpace( key ) )
            {
                throw new ArgumentException( nameof( key ) );
            }

            if ( value == null )
            {
                throw new ArgumentNullException( nameof( value ) );
            }

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            GetOrCreateHeaderValueList(key).Add( value );
        }

        public void Add( string key , IEnumerable<string> values )
        {
            if ( key == null )
            {
                throw new ArgumentNullException( nameof( key ) );
            }

            if ( string.IsNullOrWhiteSpace( key ) )
            {
                throw new ArgumentException( nameof( key ) );
            }

            if ( values == null )
            {
                throw new ArgumentNullException( nameof( values ) );
            }

            if ( values.Count() <= 0 )
            {
                throw new ArgumentException( nameof( values ) );
            }

            var headerValues = GetOrCreateHeaderValueList(key);

            foreach ( var value in values )
            {
                Add( key , value );
            }
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool ContainsKey( string key )
        {
            return _items.ContainsKey( key ?? string.Empty );
        }

        public bool Remove( string key )
        {
            return _items.Remove( key ?? string.Empty );
        }

        public bool RemoveAt( string key , int index )
        {
            if ( ! _items.TryGetValue( key ?? string.Empty , out var values ) )
            {
                return false;
            }

            if ( index < 0 || index >= values.Count )
            {
                return false;
            }

            values.RemoveAt( index );

            return true;
        }

        public bool TryAdd( string key , string value )
        {
            if ( string.IsNullOrWhiteSpace( key ) || string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            GetOrCreateHeaderValueList(key).Add( value );

            return true;
        }

        public bool TryAdd( string key , IEnumerable<string> values )
        {
            if ( string.IsNullOrWhiteSpace( key ) || values == null )
            {
                return false;
            }

            var headerValues = GetOrCreateHeaderValueList(key);
            var elementAdded = 0;

            foreach ( var value in values )
            {
                if ( string.IsNullOrWhiteSpace( value ) )
                {
                    continue;
                }

                headerValues.Add( value );
                elementAdded ++;
            }

            return elementAdded > 0;
        }

        public bool TryGetValue( string key , out string result )
        {
            result = null;

            if ( ! _items.TryGetValue( key ?? string.Empty , out var values ) )
            {
                return false;
            }

            if ( values.Count <= 0 )
            {
                return false;
            }
            
            result = values.First();

            return true;
        }

        public bool TryGetValueAt( string key , int index , out string result )
        {
            result = null;

            if ( ! _items.TryGetValue( key ?? string.Empty , out var values ) )
            {
                return false;
            }

            if ( index < 0 || index >= _items.Count )
            {
                return false;
            }
            
            result = values.ElementAt( index );

            return true;
        }

        public bool TryGetValues( string key , out string[] result )
        {
            result = _items.TryGetValue( key ?? string.Empty , out var values )
                ? values.ToArray()
                : null;

            return result != null;
        }

        private List<string> GetOrCreateHeaderValueList( string key )
        {
            System.Diagnostics.Debug.Assert( ! string.IsNullOrWhiteSpace( key ) );

            if ( ! _items.TryGetValue( key , out var headerValues ) )
            {
                _items[ key ] = ( headerValues = new List<string>() );
            }
        
            return headerValues;
        }
    }
}
