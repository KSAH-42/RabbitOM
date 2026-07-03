using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public sealed class RtspContext
    {
        public RtspContext( string method , string uri )
        {
            if ( string.IsNullOrWhiteSpace( method ) )
            {
                throw new ArgumentNullException( nameof( method ) );
            }

            if ( string.IsNullOrWhiteSpace( uri ) )
            {
                throw new ArgumentNullException( nameof( uri ) );
            }

            Method = method;

            Uri = uri;

            Parameters = new Dictionary<string, object>( StringComparer.OrdinalIgnoreCase );
        }



        public string Method { get; }

        public string Uri { get; }

        public IDictionary<string,object> Parameters { get; }




        public void Abort( string message = "operation aborted" )
        {
            throw new OperationCanceledException( message );
        }
    }
}
