using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public abstract class RtspHeaderCollection : IHeaderCollection , IReadOnlyHeaderCollection
    {
        private readonly RtspHeaderRegistry _registry;





        private protected RtspHeaderCollection( RtspHeaderRegistry registry )
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

        private protected RtspHeaderRegistry Registry
        {
            get => _registry;
        }






        public bool ContainsKey( string name )
        {
            return _registry.ContainsHeader( name );
        }

        public void Add( string name , string value )
        {
            RtspHeaderValueValidator.EnsureNotNullOrEmpty( name );

            _registry.AddHeader(
                RtspHeaderValueValidator.EnsureWellFormed( name  , RtspHeaderValueValidatorCharSet.BasicToken ) ,
                RtspHeaderValueValidator.EnsureWellFormed( value , RtspHeaderValueValidatorCharSet.BasicToken ) )
                ;
        }

        public void AddParse( string input )
        {
            _registry.AddParseHeader( RtspHeaderValueValidator.EnsureWellFormed( input  , RtspHeaderValueValidatorCharSet.BasicToken ) );
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

        IEnumerator<KeyValuePair<string , string>> IEnumerable<KeyValuePair<string , string>>.GetEnumerator()
        {
            return _registry.GetEnumerator();
        }

        public IEnumerator<KeyValuePair<string , string>> GetEnumerator()
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
            if ( ! RtspHeaderValueValidator.IsWellFormed( name , RtspHeaderValueValidatorCharSet.BasicToken ) )
            {
                return false;
            }

            if ( ! RtspHeaderValueValidator.IsWellFormed( value , RtspHeaderValueValidatorCharSet.BasicToken ) )
            {
                return false;
            }

            return _registry.TryAddHeader( name , value );
        }

        public bool TryAddParseHeader( string input )
        {
            if ( ! RtspHeaderValueValidator.IsWellFormed( input , RtspHeaderValueValidatorCharSet.BasicToken ) )
            {
                return false;
            }

            return _registry.TryAddParseHeader( input );
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
