using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class ConnectionRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Connection";





        private readonly HashSet<string> _directives = new HashSet<string>( StringComparer.OrdinalIgnoreCase );
        




        public IReadOnlyCollection<string> Directives
        {
            get => _directives;
        }
        




        public bool AddDirective( string value )
        {
            var directive = StringRtspHeaderParser.TrimValue( value , StringRtspHeaderParser.SpaceWithQuotesChars );

            if ( ! char.IsLetter( directive.FirstOrDefault() ) || ! char.IsLetterOrDigit( directive.LastOrDefault() ) )
            {
                return false;
            }

            if ( directive.Any( c => char.IsSeparator( c ) ) )
            {
                return false;
            }

            if ( directive.Any( c => char.IsPunctuation( c ) && c != '-' && c != '_' ) )
            {
                return false;
            }

            return _directives.Add( directive );
        }

        public bool RemoveDirective( string value )
        {
            return _directives.Remove( StringRtspHeaderParser.TrimValue( value , StringRtspHeaderParser.SpaceWithQuotesChars ) );
        }

        public void ClearDirectives()
        {
            _directives.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _directives );
        }






        public static bool TryParse( string input , out ConnectionRtspHeader result )
        {
            result = null;

            if ( StringRtspHeaderParser.TryParse( input , "," , out var tokens ) )
            {
                var header = new ConnectionRtspHeader();

                foreach ( var token in tokens )
                {
                    header.AddDirective( token );
                }

                if ( header.Directives.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }
    }
}
