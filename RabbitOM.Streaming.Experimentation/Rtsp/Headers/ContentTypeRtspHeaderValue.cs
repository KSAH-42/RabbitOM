using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes;
    
    public sealed class ContentTypeRtspHeaderValue
    {
        public MediaTypeWithQualityRtspHeaderValueCollection Values { get; } = new MediaTypeWithQualityRtspHeaderValueCollection();
        
        public static bool TryParse( string input , out ContentTypeRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new ContentTypeRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( MediaTypeWithQualityRtspHeaderValue.TryParse( token , out var element ) )
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
