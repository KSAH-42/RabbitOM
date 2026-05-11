using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;

    public sealed class AcceptEncodingRtspHeaderValue
    {
        public StringWithQualityRtspHeaderValueCollection Formats { get; } = new StringWithQualityRtspHeaderValueCollection();
        
        public override string ToString()
        {
            return string.Join( ", " , Formats );
        }

        public static bool TryParse( string input , out AcceptEncodingRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new AcceptEncodingRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( StringWithQualityRtspHeaderValue.TryParse( token , out var element ) )
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
