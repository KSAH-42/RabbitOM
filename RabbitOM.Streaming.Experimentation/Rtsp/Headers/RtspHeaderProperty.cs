using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal struct RtspHeaderProperty
    {
        public string Name { get; private set; }

        public string Value { get; private set; }


        public static bool TryParse( string input , string separator , out RtspHeaderProperty result )
        {
            result = default;

            if ( string.IsNullOrWhiteSpace( input ) || string.IsNullOrWhiteSpace( separator ) )
            {
                return false;
            }

            if ( ! input.Contains( separator ) )
            {
                return false;
            }

            if ( ! RtspHeaderParser.TryParse( input , separator , out string[] tokens ) )
            {
                return false;
            }

            result = new RtspHeaderProperty()
            {
                Name  = RtspHeaderParser.Formatter.UnQuote( tokens.ElementAtOrDefault( 0 ) ) ,
                Value = RtspHeaderParser.Formatter.UnQuote( tokens.ElementAtOrDefault( 1 ) ) ,
            };

            return true;
        }
    }
}
