using System;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public abstract class RtspHeaderCollection : IEnumerable , IHeaderCollection , IReadOnlyHeaderCollection
    {
        private readonly RtspHeaderService _service;




        internal RtspHeaderCollection( RtspHeaderService service )
        {
            _service = service ?? throw new ArgumentNullException( nameof( service ) );
        }




        public string this[ string name ]
        {
            get => _service.GetHeaderValue( name )?.ToString() ?? throw new InvalidOperationException();
        }

        public string this[ string name , int index]
        {
            get => _service.GetHeaderValue( name , index )?.ToString() ?? throw new InvalidOperationException();
        }





        public object SyncRoot
        {
            get => _service;
        }
        
        public int Count
        {
            get => _service.CountHeaders();
        }
        
        public IEnumerable<string> AllKeys
        {
            get => _service.Headers.Keys;
        }

        internal RtspHeaderService Service
        {
            get => _service;
        }

        public bool IsSynchronized
        {
            get => false;
        }

        





        public bool ContainsKey( string name )
        {
            return _service.ContainsHeader( name );
        }

        public void Add( string name , string value )
        {
            _service.AddHeader( name , value );
        }

        public bool Remove( string name )
        {
            return _service.RemoveHeader( name );
        }

        public bool RemoveAt( string name , int index )
        {
            return _service.RemoveHeader( name , index );
        }

        public void Clear()
        {
            _service.RemoveHeaders();
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
            return _service.GetHeaderValue( name )?.ToString() ?? string.Empty;
        }

        public string GetValueAt( string name , int index )
        {
            return _service.GetHeaderValue( name , index )?.ToString() ?? string.Empty;
        }

        public IEnumerable<string> GetValues( string name )
        {
            return _service.GetHeaderValues( name );
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

        public bool TryGetValues( string name , out IEnumerable<string> values )
        {
            return _service.TryGetHeaderValues( name , out values );
        }

        IEnumerator<KeyValuePair<string , string>> IEnumerable<KeyValuePair<string , string>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }






        // Flatten the dictionary in order to reduce allocation and preserve readonly access instead of using IEnumerator<KeyValuePair<string , string[]> or IEnumerator<KeyValuePair<string , IReadOnlyColletion<string>> where cast be apply
        public struct Enumerator : IEnumerator , IEnumerator<KeyValuePair<string,string>>
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




            public object Current
            {
                get => _current;
            }

            KeyValuePair<string,string> IEnumerator<KeyValuePair<string,string>>.Current
            {
                get => _current ;
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
