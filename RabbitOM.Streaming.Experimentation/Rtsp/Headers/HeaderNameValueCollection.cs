using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal sealed class HeaderNameValueCollection : IEnumerable , IEnumerable<KeyValuePair<string , string[]>>
    {
        private readonly Dictionary<string,List<HeaderNameValueItem>> _items = new Dictionary<string, List<HeaderNameValueItem>>( StringComparer.OrdinalIgnoreCase );
        



        public int Count
        {
            get => _items.Count;
        }

        public bool IsEmpty
        {
            get => _items.Count == 0;
        }

        public string[] AllKeys
        {
            get => _items.Keys.ToArray();
        }
        



        public struct Enumerator : IEnumerator<KeyValuePair<string , string[]>>
        {
            private readonly IEnumerator<KeyValuePair<string,List<HeaderNameValueItem>>> _enumerator;

            private KeyValuePair<string,string[]> _current;
            
            internal Enumerator( HeaderNameValueCollection collection )
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
                    _current = new KeyValuePair<string, string[]>( _enumerator.Current.Key , _enumerator.Current.Value.Select( x => x.ToString() ).ToArray() );
                
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

        public string[] GetValues( string name )
        {
            return TryGetValues( name , out var result ) ? result : Array.Empty<string>();
        }

        public string GetValueAt( string name , int index )
        {
            return TryGetValueAt( name , index , out var result ) ? result : null;
        }

        public bool ContainsKey( string key )
        {
            return _items.ContainsKey( key ?? string.Empty );
        }

        public void Add( string key , string value )
        {
            if ( string.IsNullOrWhiteSpace( key ) )
            {
                throw new ArgumentException( nameof( key ) );
            }

            if ( value == null )
            {
                throw new ArgumentNullException( nameof( value ) );
            }

            GetOrCreateHeaderValueList(key).Add( new HeaderNameValueItem( value ) );
        }

        public bool Remove( string key )
        {
            return _items.Remove( key ?? string.Empty );
        }

        public bool RemoveAt( string key , int index )
        {
            key = key ?? string.Empty ;

            if ( ! _items.TryGetValue( key , out var values ) )
            {
                return false;
            }

            if ( index < 0 || index >= values.Count )
            {
                return false;
            }

            values.RemoveAt( index );

            if ( values.Count == 0 )
            {
                _items.Remove( key );
            }

            return true;
        }

        public void Clear()
        {
            _items.Clear();
        }

        public void CopyTo( Array array , int index )
        {
            Array.Copy( _items.ToArray() , 0 , array , 0 , _items.Count );
        }

        public bool TryAdd( string key , string value )
        {
            if ( string.IsNullOrWhiteSpace( key ) || string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            GetOrCreateHeaderValueList(key).Add( new HeaderNameValueItem( value ) );

            return true;
        }

        public bool TryGetValue( string key , out string result )
        {
            result = _items.TryGetValue( key ?? string.Empty , out var values ) ? values.FirstOrDefault().ToString() : null;

            return result != null;
        }

        public bool TryGetValueAt( string key , int index , out string result )
        {
            result = _items.TryGetValue( key ?? string.Empty , out var values ) ? values.ElementAtOrDefault( index ).ToString() : null;

            return result != null;
        }

        public bool TryGetValues( string key , out string[] result )
        {
            result = _items.TryGetValue( key ?? string.Empty , out var values ) ? values.Select( x => x.ToString() ).ToArray() : null;

            return result != null;
        }
        



        private List<HeaderNameValueItem> GetOrCreateHeaderValueList( string key )
        {
            System.Diagnostics.Debug.Assert( ! string.IsNullOrWhiteSpace( key ) );

            if ( ! _items.TryGetValue( key , out var headerValues ) )
            {
                _items[ key ] = ( headerValues = new List<HeaderNameValueItem>() );
            }
        
            return headerValues;
        }


        private struct HeaderNameValueItem
        {
            public HeaderNameValueItem( object value ) => Value = value;
            public object Value { get; }
            public override string ToString() => Value?.ToString() ?? string.Empty;
        }
    }
}
