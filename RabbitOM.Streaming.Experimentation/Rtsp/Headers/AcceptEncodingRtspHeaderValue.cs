using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Compliances;

    public sealed class AcceptEncodingRtspHeaderValue
    {
        private static readonly StringValueNormalizer ValueNormalizer = StringValueNormalizer.TrimWithUnQuoteNormalizer;

        public StringWithQualityCollection Formats { get; } = new StringWithQualityCollection( IsValidFormat );
        
        public override string ToString()
        {
            return string.Join( ", " , Formats );
        }

        public static bool IsValidFormat( StringWithQuality format )
        {
            return format != null && SupportedTypes.IsEncodingSupported( format.Value );
        }

        public static bool TryParse( string input , out AcceptEncodingRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new AcceptEncodingRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( StringWithQuality.TryParse( ValueNormalizer.Normalize( token ) , out var element ) )
                    {
                        header.Formats.TryAdd( element );
                    }
                }

                if ( header.Formats.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }
    }
}
