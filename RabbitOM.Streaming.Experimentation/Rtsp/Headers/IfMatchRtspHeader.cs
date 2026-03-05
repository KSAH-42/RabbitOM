using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers;

    public sealed class IfMatchRtspHeader : RtspHeader
    {
        private readonly HashSet<string> _entitiesTags = new HashSet<string>( StringComparer.OrdinalIgnoreCase );






        public static string TypeName { get; } = "If-Match";

        public IReadOnlyCollection<string> EntitiesTags
        {
            get => _entitiesTags;
        }









        public bool AddEntityTag( string etag )
        {
            var value = StringRtspHeaderParser.TrimValue( etag , StringRtspHeaderParser.SpaceWithQuotesChars );

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            return _entitiesTags.Add( value );
        }

        public bool RemoveEntityTag( string etag )
        {
            return _entitiesTags.Remove( StringRtspHeaderParser.TrimValue( etag , StringRtspHeaderParser.SpaceWithQuotesChars ) );
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

            if ( StringRtspHeaderParser.TryParse( input , "," , out var tokens ) )
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
