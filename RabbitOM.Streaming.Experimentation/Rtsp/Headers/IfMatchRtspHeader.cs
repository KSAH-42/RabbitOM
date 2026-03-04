using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class IfMatchRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "If-Match";






        private readonly HashSet<string> _entitiesTags = new HashSet<string>( StringComparer.OrdinalIgnoreCase );







        public IReadOnlyCollection<string> EntitiesTags
        {
            get => _entitiesTags;
        }









        public bool AddEntityTag( string etag )
        {
            var value = RtspHeaderParser.Formatter.Filter( etag );

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            return _entitiesTags.Add( value );
        }

        public bool RemoveEntityTag( string etag )
        {
            return _entitiesTags.Remove( RtspHeaderParser.Formatter.Filter( etag ) );
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

            if ( RtspHeaderParser.TryParse( RtspHeaderParser.Formatter.Filter( input ) , "," , out var tokens ) )
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
