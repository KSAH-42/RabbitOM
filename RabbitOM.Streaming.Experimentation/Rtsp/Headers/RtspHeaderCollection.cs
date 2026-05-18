using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
            get => _service.GetHeaderValue( name )?.ToString() ?? throw new KeyNotFoundException();
        }

        public string this[ string name , int index]
        {
            get => _service.GetHeaderValue( name , index )?.ToString() ?? throw new KeyNotFoundException();
        }












        public object SyncRoot
        {
            get => _service;
        }
        
        public int Count
        {
            get => _service.Headers.Count;
        }
        
        public IEnumerable<string> AllKeys
        {
            get => _service.Headers.Keys;
        }

        public bool IsSynchronized
        {
            get => false;
        }

        internal RtspHeaderService Service
        {
            get => _service;
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
            var items = _service.Headers.ToArray();

            items.CopyTo( array , index );
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator( this );
        }

        public IEnumerator<KeyValuePair<string , string>> GetEnumerator()
        {
            return new Enumerator( this );
        }

        IEnumerator<KeyValuePair<string , string>> IEnumerable<KeyValuePair<string , string>>.GetEnumerator()
        {
            return new Enumerator( this );
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

        





        public struct Enumerator : IEnumerator , IEnumerator<KeyValuePair<string,string>>
        {
            private readonly IEnumerator<KeyValuePair<string,IList<object>>> _enumerator;
            private IEnumerator<object> _valuesEnumerator;
            private KeyValuePair<string,string> _current;




            internal Enumerator( RtspHeaderCollection collection )
            {
                _enumerator = collection.Service.Headers.GetEnumerator();
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

            


            public bool MoveNext()
            {
                while ( true ) 
                {
                    if ( _valuesEnumerator != null && _valuesEnumerator.MoveNext() )
                    {
                        break;
                    }

                    if ( ! _enumerator.MoveNext() )
                    {
                        return false;
                    }

                    _valuesEnumerator = _enumerator.Current.Value.GetEnumerator();
                }

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
