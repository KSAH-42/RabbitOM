using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Filters;

    public sealed class ConnectionRtspHeader
    {
        public static readonly string TypeName = "Connection";

        public static readonly StringRtspHeaderFilter ValueFilter = StringRtspHeaderFilter.UnQuoteFilter;
        

        private readonly StringRtspHashSet _directives = new StringRtspHashSet();
        

        public IReadOnlyCollection<string> Directives
        {
            get => _directives;
        }
        




        public bool AddDirective( string value )
        {
            var directive = ValueFilter.Filter( value );

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
            return _directives.Remove( ValueFilter.Filter( value ) );
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

            if ( RtspHeaderParser.TryParse( input , "," , out var tokens ) )
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
