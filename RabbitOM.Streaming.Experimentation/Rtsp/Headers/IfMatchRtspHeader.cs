using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;

    public sealed class IfMatchRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "If-Match";

        public static readonly StringValueAdapter ValueAdapter = StringValueAdapter.TrimWithUnQuoteAdapter;


        private readonly StringRtspHashSet _entitiesTags = new StringRtspHashSet();


        public IReadOnlyCollection<string> EntitiesTags
        {
            get => _entitiesTags;
        }


        public bool AddEntityTag( string etag )
        {
            return _entitiesTags.Add( ValueAdapter.Adapt( etag ) );
        }

        public bool RemoveEntityTag( string etag )
        {
            return _entitiesTags.Remove( ValueAdapter.Adapt( etag ) );
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

            if ( RtspHeaderParser.TryParse( input , "," , out string[] tokens ) )
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
