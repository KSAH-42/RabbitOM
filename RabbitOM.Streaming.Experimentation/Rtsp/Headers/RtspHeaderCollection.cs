using System;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public abstract class RtspHeaderCollection : IHeaderCollection , IReadOnlyHeaderCollection
    {
        private readonly RtspHeaderService _service;




        internal RtspHeaderCollection( RtspHeaderService service )
        {
            _service = service ?? throw new ArgumentNullException( nameof( service ) );
        }




        public string this[ string name ]
        {
            get => _service.GetHeaderValueByName( name );
        }

        public string this[ string name , int index]
        {
            get => _service.GetHeaderValueByName( name , index );
        }





        public object SyncRoot
        {
            get => _service;
        }
        
        public int Count
        {
            get => _service.CountHeaders();
        }
        
        public string[] AllKeys
        {
            get => _service.GetHeaderKeys();
        }

        public bool IsSynchronized
        {
            get => false;
        }

        internal RtspHeaderService Service
        {
            get => _service;
        }





        public void Add( string name , string value )
        {
            _service.AddHeader( name , value );
        }

        public bool ContainsKey( string name )
        {
            return _service.ContainsHeader( name );
        }

        public void CopyTo( Array array , int index )
        {
            _service.CopyHeadersTo( array , index );
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator( this._service );
        }

        public IEnumerator<KeyValuePair<string , string>> GetEnumerator()
        {
            return new Enumerator( this._service );
        }

        public string GetValue( string name )
        {
            return _service.GetHeaderValueByName( name );
        }

        public string GetValueAt( string name , int index )
        {
            return _service.GetHeaderValueByName( name , index );
        }

        public string[] GetValues( string name )
        {
            return _service.GetHeaderValuesByName( name );
        }

        public bool Remove( string name )
        {
            return _service.RemoveHeaderByName( name );
        }

        public bool RemoveAt( string name , int index )
        {
            return _service.RemoveHeaderByName( name , index );
        }

        public bool TryAdd( string name , string value )
        {
            return _service.TryAddHeader( name , value );
        }

        public bool TryGetValue( string name , out string value )
        {
            return _service.TryGetHeaderValue( name , out value );
        }

        public bool TryGetValueAt( string name , int index , out string value )
        {
            return _service.TryGetHeaderValue( name , index , out value );
        }

        public bool TryGetValues( string name , out string[] values )
        {
            return _service.TryGetHeaderValues( name , out values );
        }






        // Flatten the dictionary in order to reduce allocation and preserve readonly access instead of using IEnumerator<KeyValuePair<string , string[]> or IEnumerator<KeyValuePair<string , IReadOnlyColletion<string>> where cast be apply
        public struct Enumerator : IEnumerator , IEnumerator<KeyValuePair<string , string>>
        {
            private readonly IEnumerator<KeyValuePair<string,IList<object>>> _enumerator;
            private IEnumerator<object> _valuesEnumerator;
            private KeyValuePair<string,string> _current;

            internal Enumerator( RtspHeaderService service )
            {
                _enumerator = service.Headers.GetEnumerator();
                _valuesEnumerator = null;
                _current = default;
            }

            KeyValuePair<string , string> IEnumerator<KeyValuePair<string , string>>.Current
            {
                get => _current ;
            }

            public object Current
            {
                get => _current;
            }

            // => for upper level using foreach time complexity could be:

            // O(n) = O(1 + 3K) + O(T) => O(K + T)
            // O (n *(K + T) ) => O(n²) where T could be equal to n
            // or
            // Ω(n) => add a struct with toString and change the interface to get O(n + P) = Ω(n)
            // => so replace (???) the KeyValuePair<string,string> by the KeyValuePair<string,RtspHeaderValue> where RtspHeaderValue is a struct and to string will switch between the raw string value or the header value class and the to string can be called out size the MoveNext method

            public bool MoveNext()
            {
                // O(1 + 3K) => O(K)
                while ( true ) 
                {
                    // O(1)
                    if ( _valuesEnumerator != null && _valuesEnumerator.MoveNext() )
                    {
                        break;
                    }

                    // O(1)
                    if ( ! _enumerator.MoveNext() )
                    {
                        return false;
                    }

                    // O(1)
                    _valuesEnumerator = _enumerator.Current.Value.GetEnumerator();
                }

                // O(1 + n) => O(T)
                _current = new KeyValuePair<string,string>( _enumerator.Current.Key , _valuesEnumerator.Current?.ToString() ?? string.Empty );

                return true;
            }

            public void Reset()
            {
                _enumerator.Reset();
                _valuesEnumerator = null;
                _current = default;
            }

            public void Dispose()
            {
                _enumerator.Dispose();
                _valuesEnumerator?.Dispose();
                _valuesEnumerator = null;
                _current = default;
            }
        }
    }
}
