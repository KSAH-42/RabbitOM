using System;
using System.Collections.Generic;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public sealed class RtspClientRequest
    {
        private readonly Dictionary<string,string> _headers = new Dictionary<string, string>( StringComparer.OrdinalIgnoreCase );

        private readonly MemoryStream _content = new MemoryStream();




        public IReadOnlyDictionary<string,string> Headers
        {
            get => _headers;
        }

        public long ContentLength
        {
            get => _content.Length;
        }




        public void AddHeader( string name , string value )
        {
            throw new NotImplementedException();
        }

        public void AddHeader( string name , long value )
        {
            throw new NotImplementedException();
        }

        public void AddHeader( string name , DateTime value )
        {
            throw new NotImplementedException();
        }

        public bool TryAddHeader( string name , string value )
        {
            throw new NotImplementedException();
        }

        public bool TryAddHeader( string name , long value )
        {
            throw new NotImplementedException();
        }

        public bool TryAddHeader( string name , DateTime value )
        {
            throw new NotImplementedException();
        }

        public bool TryAddOrUpdateHeader( string name , string value )
        {
            throw new NotImplementedException();
        }

        public bool TryAddOrUpdateHeader( string name , long value )
        {
            throw new NotImplementedException();
        }

        public bool TryAddOrUpdateHeader( string name , DateTime value )
        {
            throw new NotImplementedException();
        }

        public void RemoveHeader( string name )
        {
            throw new NotImplementedException();
        }

        public void ClearHeaders()
        {
            throw new NotImplementedException();
        }

        public void WriteContent( string value )
        {
            throw new NotImplementedException();
        }

        public void WriteContent( byte[] value )
        {
            throw new NotImplementedException();
        }

        public void ClearContent()
        {
            throw new NotImplementedException();
        }
    }
}
