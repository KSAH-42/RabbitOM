using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes;
    
    public sealed class AcceptRtspHeaderValue
    {
        public StringWithQualityRtspHeaderValueCollection Mimes { get; } = new StringWithQualityRtspHeaderValueCollection( mime =>
        {
            return SupportedTypes.IsMimeSupported( mime.Value );
        } );
        
        public static bool TryParse( string input , out AcceptRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new AcceptRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( StringWithQualityRtspHeaderValue.TryParse( token , out var element ) )
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

        public override string ToString()
        {
            return string.Join( ", " , Mimes );
        }
    }
}
