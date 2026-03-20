using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;

    public sealed class ConnectionRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Connection";

        public static readonly StringValueAdapter ValueAdapter = StringValueAdapter.TrimWithUnQuoteAdapter;
        

        private readonly HashSet<string> _directives = new HashSet<string>( StringComparer.OrdinalIgnoreCase );
        

        public IReadOnlyCollection<string> Directives
        {
            get => _directives;
        }
        
        public static bool TryParse( string input , out ConnectionRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new ConnectionRtspHeaderValue();

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


        public bool AddDirective( string value )
        {
            var directive = ValueAdapter.Adapt( value );

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
            return _directives.Remove( ValueAdapter.Adapt( value ) );
        }

        public void RemoveDirectives()
        {
            _directives.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _directives );
        }
    }
}
