using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types.Compliances;

    public sealed class AcceptRtspHeaderValue
    {
        private static readonly StringValueNormalizer ValueNormalizer = StringValueNormalizer.TrimWithUnQuoteNormalizer;

        public StringWithQualityCollection Mimes { get; } = new StringWithQualityCollection( IsValidMime );
        
        public override string ToString()
        {
            return string.Join( ", " , Mimes );
        }

        public static bool IsValidMime( StringWithQuality encoding )
        {
            return encoding != null && SupportedTypes.IsMimeSupported( encoding.Value );
        }

        public static bool TryParse( string input , out AcceptRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new AcceptRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( StringWithQuality.TryParse( ValueNormalizer.Normalize( token ) , out var element ) )
                    {
                        header.Mimes.TryAdd( element );
                    }
                }

                if ( header.Mimes.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }
    }
}
