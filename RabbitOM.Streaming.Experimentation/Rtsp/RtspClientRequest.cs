using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
    
    public sealed class RtspClientRequest
    {
        private readonly RtspHeaderCollection _headers = new RtspHeaderCollection();

        private readonly MemoryStream _content = new MemoryStream();




        public IReadOnlyHeaderCollection Headers
        {
            get => _headers;
        }

        public long ContentLength
        {
            get => _content.Length;
        }




        public void AddHeader( string name , RtspHeaderValue value )
        {
            _headers.Add( name , value );
        }

        public bool TryAddHeader( string name , RtspHeaderValue value )
        {
            return _headers.TryAdd( name , value );
        }

        public void RemoveHeader( string name )
        {
            _headers.Remove( name );
        }

        public void RemoveHeader( string name , int index )
        {
            _headers.RemoveAt( name , index );
        }

        public void ClearHeaders()
        {
            _headers.Clear();
        }

        public void WriteContent( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return;
            }

            var buffer = System.Text.Encoding.UTF8.GetBytes( value );

            _content.Write( buffer , 0 , buffer.Length );
        }

        public void WriteContent( byte[] value )
        {
            if ( value == null || value.Length <= 0 )
            {
                return;
            }

            _content.Write( value , 0 , value.Length );
        }

        public void ClearContent()
        {
            _content.SetLength(0);
        }
    }
}
