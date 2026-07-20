using System;

namespace RabbitOM.Streaming.RtspV2.Headers
{
    using RabbitOM.Streaming.RtspV2.Headers.DataTypes;

    public sealed class AcceptLanguageRtspHeaderValue
    {
        public LanguageWithQualityRtspHeaderValueCollection Values { get; } = new LanguageWithQualityRtspHeaderValueCollection();

        public static bool TryParse( string input , out AcceptLanguageRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new AcceptLanguageRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( LanguageWithQualityRtspHeaderValue.TryParse( token , out var element ) )
                    {
                        header.Values.TryAdd( element );
                    }
                }

                if ( header.Values.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }

        public override string ToString()
        {
            return string.Join( ", " , Values );
        }
    }
}
