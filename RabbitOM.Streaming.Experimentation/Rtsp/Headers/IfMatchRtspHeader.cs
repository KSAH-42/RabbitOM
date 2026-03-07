using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Filters;

    public sealed class IfMatchRtspHeader
    {
        public static readonly string TypeName = "If-Match";

        public static readonly StringRtspHeaderFilter ValueFilter = StringRtspHeaderFilter.UnQuoteFilter;


        private readonly StringRtspHashSet _entitiesTags = new StringRtspHashSet();


        public IReadOnlyCollection<string> EntitiesTags
        {
            get => _entitiesTags;
        }


        public bool AddEntityTag( string etag )
        {
            return _entitiesTags.Add( ValueFilter.Filter( etag ) );
        }

        public bool RemoveEntityTag( string etag )
        {
            return _entitiesTags.Remove( ValueFilter.Filter( etag ) );
        }

        public void ClearEntitiesTags()
        {
            _entitiesTags.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _entitiesTags.Select( element => $"\"{element}\"" ) );
        }








        public static bool TryParse( string input , out IfMatchRtspHeader result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , "," , out var tokens ) )
            {
                var header = new IfMatchRtspHeader();

                foreach ( var token in tokens )
                {
                    header.AddEntityTag( token );
                }
            
                if ( header.EntitiesTags.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }
    }
}
