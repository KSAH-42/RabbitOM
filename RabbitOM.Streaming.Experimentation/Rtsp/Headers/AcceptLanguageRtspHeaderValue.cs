using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;
    
    public sealed class AcceptLanguageRtspHeaderValue
    {
        public StringWithQualityRtspHeaderValueCollection Cultures { get; } = new StringWithQualityRtspHeaderValueCollection( culture =>
        {
            return SupportedTypes.IsLanguageSupported( culture.Value );
        } );
        
        public static bool TryParse( string input , out AcceptLanguageRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new AcceptLanguageRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( StringWithQualityRtspHeaderValue.TryParse( token , out var element ) )
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

        public override string ToString()
        {
            return string.Join( ", " , Cultures );
        }
    }
}
