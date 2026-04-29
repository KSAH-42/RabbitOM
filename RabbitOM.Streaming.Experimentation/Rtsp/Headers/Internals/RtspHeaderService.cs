using System;
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





        public void AddHeader( string name , string value )
        {
            // ensure if not a forbidden header
            //  no: throw 
            // check if the parser is registered 
            //  no: queue the header as string
            //  yes: parse and queue the header value class

            throw new NotImplementedException();
        }

        public bool ContainsHeader( string name )
        {
            throw new NotImplementedException();
        }

        public void CopyHeadersTo( Array array , int index )
        {
            throw new NotImplementedException();
        }

        public int CountHeaders()
        {
            throw new NotImplementedException();
        }

        public void SetHeaderValue( string typeName , object value )
        {
            throw new NotImplementedException();
        }

        public object GetHeaderValue( string typeName )
        {
            throw new NotImplementedException();
        }

        public string GetHeaderValueByName( string name )
        {
            throw new NotImplementedException();
        }

        public string[] GetHeaderKeys()
        {
            throw new NotImplementedException();
        }

        public string GetHeaderValueByName( string name , int index )
        {
            throw new NotImplementedException();
        }

        public string[] GetHeaderValuesByName( string name )
        {
            throw new NotImplementedException();
        }

        public bool RemoveHeaderByName( string name )
        {
            throw new NotImplementedException();
        }

        public bool RemoveHeaderByName( string name , int index )
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

        public bool TryGetHeaderValues( string name , out string[] values )
        {
            throw new NotImplementedException();
        }
    }
}
