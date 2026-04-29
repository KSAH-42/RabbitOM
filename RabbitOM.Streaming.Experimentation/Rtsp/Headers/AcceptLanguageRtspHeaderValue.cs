using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Compliances;

    public sealed class AcceptLanguageRtspHeaderValue
    {
        private static readonly StringValueNormalizer ValueNormalizer = StringValueNormalizer.TrimWithUnQuoteNormalizer;

        public StringWithQualityCollection Cultures { get; } = new StringWithQualityCollection( IsValidLanguage );
        
        public override string ToString()
        {
            return string.Join( ", " , Cultures );
        }

        public static bool IsValidLanguage( StringWithQuality encoding )
        {
            return encoding != null && SupportedTypes.IsLanguageSupported( encoding.Value );
        }

        public static bool TryParse( string input , out AcceptLanguageRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new AcceptLanguageRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( StringWithQuality.TryParse( ValueNormalizer.Normalize( token ) , out var element ) )
                    {
                        header.Cultures.TryAdd( element );
                    }
                }

                if ( header.Cultures.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }
    }
}
