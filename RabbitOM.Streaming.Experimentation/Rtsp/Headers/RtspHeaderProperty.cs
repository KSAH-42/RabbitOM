using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;

    internal struct RtspHeaderProperty
    {
        public string Name { get; private set; }

        public string Value { get; private set; }

        public static bool TryParse( string input , string separator , out RtspHeaderProperty result )
        {
            result = default;

            if ( string.IsNullOrWhiteSpace( input ) || string.IsNullOrWhiteSpace( separator ) || ! input.Contains( separator ) )
            {
                return false;
            }

            if ( RtspHeaderParser.TryParse( input , separator , out string[] tokens ) )
            {
                result = new RtspHeaderProperty()
                {
                    Name  = StringValueAdapter.UnQuoteAdapter.Adapt( tokens.ElementAtOrDefault( 0 ) ) ,
                    Value = StringValueAdapter.UnQuoteAdapter.Adapt( tokens.ElementAtOrDefault( 1 ) ) ,
                };

                return true;
            }

            return false;
        }
    }
}
