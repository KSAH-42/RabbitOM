using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal sealed class RtspHeaderService
    {
        private readonly RtspHeaderServiceSettings _settings;

        private readonly Dictionary<string,IList<object>> _headers;





        public RtspHeaderService( RtspHeaderServiceSettings settings )
        {
            _settings = settings ?? throw new ArgumentNullException( nameof( settings ) );

            _headers = new Dictionary<string, IList<object>>( StringComparer.OrdinalIgnoreCase );
        }





        public RtspHeaderServiceSettings Settings
        {
            get => _settings;
        }

        public IReadOnlyDictionary<string,IList<object>> Headers
        {
            get => _headers;
        }





        public bool ContainsHeader( string name )
        {
            return _headers.ContainsKey( name ?? string.Empty );
        }

        public void CopyHeadersTo( Array array , int index )
        {
            throw new NotImplementedException();
        }

        public int CountHeaders()
        {
            return _headers.Count;
        }

        public void AddHeader( string name , string value )
        {
            // ensure if not a forbidden header
            //  no: throw 
            // check if the parser is registered 
            //  no: queue the header as string
            //  yes: parse and queue the header value class

            throw new NotImplementedException();
        }

        public bool RemoveHeader( string name )
        {
            return _headers.Remove( name ?? string.Empty );
        }

        public bool RemoveHeader( string name , int index )
        {
            if ( ! _headers.TryGetValue( name ?? string.Empty , out var values ) )
            {
                return false;
            }

            Debug.Assert( values != null );

            if ( index < 0 || index >= values.Count )
            {
                return false;
            }

            values.RemoveAt( index );

            return true;
        }

        public void RemoveHeaders()
        {
            _headers.Clear();
        }

        public void SetHeaderValue( string typeName , object value )
        {
            // TODO: clear the list add the new one 
            throw new NotImplementedException();
        }

        public object GetHeaderValue( string name )
        {
            throw new NotImplementedException();
        }

        public object GetHeaderValue( string name , int index )
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetHeaderValues( string name )
        {
            throw new NotImplementedException();
        }

        public bool TryAddHeader( string name , string value )
        {
            throw new NotImplementedException();
        }

        public bool TryGetHeaderValue( string name , out string value )
        {
            throw new NotImplementedException();
        }

        public bool TryGetHeaderValue( string name , int index , out string value )
        {
            throw new NotImplementedException();
        }

        public bool TryGetHeaderValues( string name , out IEnumerable<string> values )
        {
            throw new NotImplementedException();
        }
    }
}
