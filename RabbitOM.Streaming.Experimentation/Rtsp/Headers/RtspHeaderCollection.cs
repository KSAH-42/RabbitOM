using System;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public abstract class RtspHeaderCollection : IEnumerable , IHeaderCollection , IReadOnlyHeaderCollection
    {
        private readonly RtspHeaderRegistry _registry;








        internal RtspHeaderCollection( RtspHeaderRegistry registry )
        {
            _registry = registry ?? throw new ArgumentNullException( nameof( registry ) );
        }









        public string this[ string name ]
        {
            get => _registry.GetHeaderValue( name );
        }

        public string this[ string name , int index]
        {
            get => _registry.GetHeaderValue( name , index );
        }












        public object SyncRoot
        {
            get => _registry;
        }
        
        public int Count
        {
            get => _registry.CountHeaders();
        }
        
        public IEnumerable<string> AllKeys
        {
            get => _registry.GetHeaderNames();
        }

        public bool IsSynchronized
        {
            get => false;
        }

        internal RtspHeaderRegistry Registry
        {
            get => _registry;
        }

        
        









        public bool ContainsKey( string name )
        {
            return _registry.ContainsHeader( name );
        }

        public void Add( string name , string value )
        {
            _registry.AddHeader( RtspHeaderValueValidator.EnsureWellFormed( name ) , RtspHeaderValueValidator.EnsureWellFormedTokenOrEmpty( value ) );
        }

        public bool Remove( string name )
        {
            return _registry.RemoveHeader( name );
        }

        public bool RemoveAt( string name , int index )
        {
            return _registry.RemoveHeader( name , index );
        }

        public void Clear()
        {
            _registry.ClearHeaders();
        }
        
        public void CopyTo( Array array , int index )
        {
            _registry.CopyTo( array , index );
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _registry.GetEnumerator();
        }

        public IEnumerator<KeyValuePair<string , string>> GetEnumerator()
        {
            return _registry.GetEnumerator();
        }

        IEnumerator<KeyValuePair<string , string>> IEnumerable<KeyValuePair<string , string>>.GetEnumerator()
        {
            return _registry.GetEnumerator();
        }

        public string GetValue( string name )
        {
            return _registry.GetHeaderValue( name );
        }

        public string GetValueAt( string name , int index )
        {
            return _registry.GetHeaderValue( name , index );
        }

        public IEnumerable<string> GetValues( string name )
        {
            return _registry.GetHeaderValues( name );
        }

        public bool TryAdd( string name , string value )
        {
            if ( ! RtspHeaderValueValidator.TryEnsureWellFormed( name ) || ! RtspHeaderValueValidator.TryEnsureWellFormedTokenOrEmpty( value ) )
            {
                return false;
            }

            return _registry.TryAddHeader( name , value );
        }

        public bool TryGetValue( string name , out string value )
        {
            return _registry.TryGetHeaderValue( name , out value );
        }

        public bool TryGetValueAt( string name , int index , out string value )
        {
            return _registry.TryGetHeaderValueAt( name , index , out value );
        }

        public bool TryGetValues( string name , out IEnumerable<string> values )
        {
            return _registry.TryGetHeaderValues( name , out values );
        }
    }
}
