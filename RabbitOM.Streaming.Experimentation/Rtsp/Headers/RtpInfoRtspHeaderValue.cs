using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes;

    public sealed class RtpInfoRtspHeaderValue
    {
        public RtpFieldRtspHeaderValueCollection Fields { get; } = new RtpFieldRtspHeaderValueCollection();

        public static bool TryParse( string input , out RtpInfoRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new RtpInfoRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( RtpFieldRtspHeaderValue.TryParse( token , out var info ) )
                    {
                        header.Fields.TryAdd( info );
                    }
                }

                if ( header.Fields.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }

        public override string ToString()
        {
            return string.Join( ", " , Fields );
        }
    }
}
