using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Headers
{
    public sealed class RtspHeaderNameValueCollection : IEnumerable , IEnumerable<KeyValuePair<string , RtspHeaderValue[]>>
    {
        private readonly Dictionary<string,List<RtspHeaderValue>> _items = new Dictionary<string, List<RtspHeaderValue>>( StringComparer.OrdinalIgnoreCase );
        



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
        



        public struct Enumerator : IEnumerator<KeyValuePair<string , RtspHeaderValue[]>>
        {
            private readonly IEnumerator<KeyValuePair<string,List<RtspHeaderValue>>> _enumerator;

            private KeyValuePair<string,RtspHeaderValue[]> _current;
            
            internal Enumerator( RtspHeaderNameValueCollection collection )
            {
                _enumerator = collection._items.GetEnumerator();
                _current = default;
            }

            object IEnumerator.Current
            {
                get => _current;
            }

            public KeyValuePair<string , RtspHeaderValue[]> Current
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
                    _current = new KeyValuePair<string, RtspHeaderValue[]>( _enumerator.Current.Key , _enumerator.Current.Value.ToArray() );
                
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

        public IEnumerator<KeyValuePair<string , RtspHeaderValue[]>> GetEnumerator()
        {
            return new Enumerator( this );
        }

        public RtspHeaderValue[] GetValues( string name )
        {
            return TryGetValues( name , out var result ) ? result : Array.Empty<RtspHeaderValue>();
        }
           
        public TValue GetValue<TValue>( string name ) where TValue : RtspHeaderValue
        {
            return TryGetValue( name , out var result ) ? result as TValue: null;
        }

        public TValue GetValue<TValue>( string name , Func<TValue> factory ) where TValue : RtspHeaderValue
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new ArgumentException( nameof( factory ) );
            }

            if ( factory == null )
            {
                throw new ArgumentNullException( nameof( factory ) );
            }

            TValue value = _items.TryGetValue( name , out var result ) ? result as TValue : null;
                
            if ( value == null )
            {
                value = factory() ?? throw new InvalidOperationException( "the factory produce a null instance" );

                var headers = GetOrCreateHeaderValueList(name);

                if ( headers.Count > 0 )
                {
                    headers.RemoveAt( 0 );
                }

                headers.Add( value );
            }

            return value;
        }

        public TValue GetValueAt<TValue>( string name , int index ) where TValue : RtspHeaderValue
        {
            return TryGetValueAt( name , index , out var result ) ? result as TValue: null;
        }

        public void SetValue<TValue>( string name , TValue value ) where TValue : RtspHeaderValue
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new ArgumentException( nameof( name ) );
            }

            var headers = GetOrCreateHeaderValueList( name );

            if ( value == null )
            {
                _items.Remove( name );
                return;
            }

            if ( headers.Count > 0 )
            {
                headers.RemoveAt( 0 );
            }

            headers.Add( value );
        }

        public bool ContainsKey( string key )
        {
            return _items.ContainsKey( key ?? string.Empty );
        }

        public void Add( string key , RtspHeaderValue value )
        {
            if ( string.IsNullOrWhiteSpace( key ) )
            {
                throw new ArgumentException( nameof( key ) );
            }

            if ( value == null )
            {
                throw new ArgumentNullException( nameof( value ) );
            }

            GetOrCreateHeaderValueList(key).Add( value );
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

            GetOrCreateHeaderValueList(key).Add( new StringRtspHeaderValue( value ) );

            return true;
        }

        public bool TryAdd( string key , RtspHeaderValue value )
        {
            if ( string.IsNullOrWhiteSpace( key ) || value == null )
            {
                return false;
            }

            GetOrCreateHeaderValueList(key).Add( value );

            return true;
        }

        public bool TryGetValue( string key , out RtspHeaderValue result )
        {
            result = _items.TryGetValue( key ?? string.Empty , out var values ) ? values.FirstOrDefault() : null;

            return result != null;
        }

        public bool TryGetValueAt( string key , int index , out RtspHeaderValue result )
        {
            result = _items.TryGetValue( key ?? string.Empty , out var values ) ? values.ElementAtOrDefault( index ) : null;

            return result != null;
        }

        public bool TryGetValues( string key , out RtspHeaderValue[] result )
        {
            result = _items.TryGetValue( key ?? string.Empty , out var values ) ? values.ToArray() : null;

            return result != null;
        }
        



        private List<RtspHeaderValue> GetOrCreateHeaderValueList( string key )
        {
            System.Diagnostics.Debug.Assert( ! string.IsNullOrWhiteSpace( key ) );

            if ( ! _items.TryGetValue( key , out var headerValues ) )
            {
                _items[ key ] = ( headerValues = new List<RtspHeaderValue>() );
            }
        
            return headerValues;
        }
    }
}
