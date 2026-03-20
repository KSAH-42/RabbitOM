using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    // TODO: adding unit tests

    public class RtspHeaderCollection : IHeaderCollection , IReadOnlyHeaderCollection
    {
        private readonly Dictionary<string,List<RtspHeaderValue>> _items = new Dictionary<string, List<RtspHeaderValue>>( StringComparer.OrdinalIgnoreCase );


        

        public RtspHeaderValue[] this[string key] 
        { 
            get => _items[ key ].ToArray();
        }

        public RtspHeaderValue this[string key,int index]
        {
            get => _items[ key ].ElementAt( index );
        }



        public object SyncRoot
        {
            get => _items;
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

        public bool IsSynchronized
        {
            get => false;
        }



        public struct Enumerator : IEnumerator<KeyValuePair<string , RtspHeaderValue[]>>
        {
            private readonly IEnumerator<KeyValuePair<string,List<RtspHeaderValue>>> _enumerator;

            private KeyValuePair<string,RtspHeaderValue[]> _current;
            

            internal Enumerator( RtspHeaderCollection collection )
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





        public void Add( string key , RtspHeaderValue value )
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

            GetOrCreateHeaderValueList(key).Add( value );
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool ContainsKey( string key )
        {
            return _items.ContainsKey( key ?? string.Empty );
        }

        public void CopyTo( Array array , int index )
        {
            Array.Copy( _items.ToArray() , 0 , array , 0 , _items.Count );
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

        public bool TryGetValueAt( string key , int index , out RtspHeaderValue result )
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

        public bool TryGetValues( string key , out RtspHeaderValue[] result )
        {
            result = _items.TryGetValue( key ?? string.Empty , out var values )
                ? values.ToArray()
                : null;

            return result != null;
        }

        public bool TryParseWithAdd( string input )
        {
            throw new NotImplementedException();
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
