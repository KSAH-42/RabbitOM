using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes;
    
    public sealed class AcceptRtspHeaderValue
    {
        public MediaTypeWithQualityRtspHeaderValueCollection MediaTypes { get; } = new MediaTypeWithQualityRtspHeaderValueCollection();
        
        public static bool TryParse( string input , out AcceptRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new AcceptRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( MediaTypeWithQualityRtspHeaderValue.TryParse( token , out var element ) )
                    {
                        header.MediaTypes.TryAdd( element );
                    }
                }

                if ( header.MediaTypes.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }

        public override string ToString()
        {
            return string.Join( ", " , MediaTypes );
        }
    }
}
