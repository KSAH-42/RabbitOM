using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public sealed class RtspContext
    {
        private Dictionary<string,object> _parameters;

        public RtspContext( string method , string uri )
        {
            Method = method;
            Uri = uri;
        }

        public string Method { get; }

        public string Uri { get; }

        public Dictionary<string,object> Parameters
        {
            get
            {
                if ( _parameters == null )
                {
                    _parameters = new Dictionary<string, object>( StringComparer.OrdinalIgnoreCase );
                }

                return _parameters;
            }
        }
    }
}
